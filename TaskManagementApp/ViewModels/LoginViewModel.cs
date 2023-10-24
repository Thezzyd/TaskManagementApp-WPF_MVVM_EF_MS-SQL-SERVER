using TaskManagementApp.GlobalCommands;
using TaskManagementApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagementApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        private string _username;
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

        public ICommand LoginUserCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToTasksMenuCommand { get; }

        public LoginViewModel(IUserService userService,ISessionService sessionService, INavigationService registerViewNavigationService, INavigationService tasksMenuNavigationService)
        {
            _userService = userService;
            _sessionService = sessionService;

            LoginUserCommand = new ViewModelAsyncCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            NavigateToRegisterCommand = new NavigateCommand(registerViewNavigationService);
            NavigateToTasksMenuCommand = new NavigateCommand(tasksMenuNavigationService);
        }

        private bool CanExecuteLoginCommand(object parameters)
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task ExecuteLoginCommand(object parameters)
        {
            try
            {
                var result = await _userService.AuthenticateUser(Username, Password);
                if (result.Success)
                {
                    SuccessMessage = result.Message;
                    _sessionService.SetCurrentUser(result.Data);
                    if (NavigateToTasksMenuCommand.CanExecute(parameters))
                    {
                        NavigateToTasksMenuCommand.Execute(parameters);
                    }
                }
                else
                {
                    ErrorMessage = result.Message;
                }

            }catch (Exception)
            {
                ErrorMessage = "Failed to authenticate";
            }
        }
    }
}
