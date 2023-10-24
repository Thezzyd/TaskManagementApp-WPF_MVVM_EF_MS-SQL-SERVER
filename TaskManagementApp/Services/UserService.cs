using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.DTOs;
using TaskManagementApp.Models;
using TaskManagementApp.Stores;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagementApp.Services
{
    internal class UserService : IUserService
    {
        private readonly AppDbContextFactory _dbContextFactory;

        private const string USERNAME_PATTERN = @"^[A-Za-z0-9_-]{3,15}$";
        private const string EMAIL_PATTERN = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        private const string PASSWORD_PATTERN = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";

        public UserService(AppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ServiceResopnse<CurrentUserDto>> AuthenticateUser(string username, string password)
        {
            using(AppDbContext _appDbContext = _dbContextFactory.CreateDbContext()) 
            {
                var serviceResopnse = new ServiceResopnse<CurrentUserDto>();
                User user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user !=null && PasswordHash.ValidatePassword(password, user.Password))
                {
                    serviceResopnse.Data = new CurrentUserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email
                    };
                    serviceResopnse.Message = "Logged in successfully.";
                    return serviceResopnse;
                }

                serviceResopnse.Success = false;
                serviceResopnse.Message = "Invalid username or password.";
                return serviceResopnse;
            }
        }

        public async Task<ServiceResopnse<User>> CreateUser(User user)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                var serviceResponse = new ServiceResopnse<User>();

                bool isUsernameValid = Regex.IsMatch(user.Username, USERNAME_PATTERN);
                if (!isUsernameValid)
                { 
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid username - too long or contains forbidden characters.";
                    return serviceResponse;
                }

                bool isEmailValid = Regex.IsMatch(user.Email, EMAIL_PATTERN);
                if (!isEmailValid)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid email address.";
                    return serviceResponse;
                }

                bool isPasswordValid = Regex.IsMatch(user.Password, PASSWORD_PATTERN);
                if (!isPasswordValid)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid password - min 8 characters, min 1 uppercase, lowercase, digit and special character.";
                    return serviceResponse;
                }

                User existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
                if (existingUser != null)
                {
                    serviceResponse.Success = false;
                    if (existingUser.Username == user.Username)
                    {
                        serviceResponse.Message = "User with this username already exists.";
                    }
                    else
                    {
                        serviceResponse.Message = "User with this email already exists.";
                    }
                    return serviceResponse;
                }

                user.Password = PasswordHash.CreateHash(user.Password);
                dbContext.Users.Add(user);
                var result = await dbContext.SaveChangesAsync() > 0;
                if (result)
                {
                    serviceResponse.Data = user;
                    serviceResponse.Message = "Account successfully created";
                    return serviceResponse;
                }

                serviceResponse.Success = false;
                serviceResponse.Message = "Problem occured when adding new account";
                return serviceResponse;
            }
        }
    }
}
