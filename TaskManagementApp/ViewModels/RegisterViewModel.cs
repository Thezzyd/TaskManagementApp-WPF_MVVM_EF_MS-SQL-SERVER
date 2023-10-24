using TaskManagementApp.Services;
using System;
using System.Windows.Input;
using TaskManagementApp.Commands;
using System.Threading.Tasks;
using TaskManagementApp.Models;

namespace TaskManagementApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        private string _username;
        private string _email;
        private string _password;
        private string _errorMessage;
        private string _successMessage;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                _successMessage = string.Empty;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }
        public string SuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                _errorMessage = string.Empty;
                OnPropertyChanged(nameof(SuccessMessage));
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand RegisterUserCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public RegisterViewModel(IUserService userService , INavigationService loginNavigationService)
        {
            _userService = userService;

            RegisterUserCommand = new ViewModelAsyncCommand(ExecuteRegisterUserCommand, CanExecuteRegisterUserCommand);
            NavigateToLoginCommand = new NavigateCommand(loginNavigationService);
        }


        private bool CanExecuteRegisterUserCommand(object parameter)
        {
            if (Username == null || Email == null || Password == null || Password.Length < 3 || Username.Length < 3 || Email.Length < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async System.Threading.Tasks.Task ExecuteRegisterUserCommand(object parameter)
        {
            try
            {
                User user = new User
                {
                    Username = Username,
                    Email = Email,
                    Password = Password
                };

                var result = await _userService.CreateUser(user);
                if (result.Success)
                {
                    SuccessMessage = result.Message;
                }
                else
                { 
                    ErrorMessage = result.Message;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to register account.";
            }
            
        }

    }
}
