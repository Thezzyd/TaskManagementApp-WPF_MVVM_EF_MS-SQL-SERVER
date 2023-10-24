using TaskManagementApp.DTOs;
using TaskManagementApp.Models;
using System.Threading.Tasks;

namespace TaskManagementApp.Services
{
    public interface IUserService
    {
        Task<ServiceResopnse<CurrentUserDto>> AuthenticateUser(string username, string password);
        Task<ServiceResopnse<User>> CreateUser(User user);
    }
}
