using TaskManagementApp.DTOs;
using TaskManagementApp.Services;
using TaskManagementApp.SharedData;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagementApp.ViewModels
{
    public class CreateTaskViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly ITaskService _taskService;
        private readonly INavigationService _closeModalNavigationService;

        private string _name;
        private string _description;
        private DateTime? _deadline;
        private bool _isDeadline;
        private string _priority;
        private string _errorMessage;
        private string _successMessage;
      
        public string Name {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public DateTime? Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }
        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
        public bool IsDeadline
        {
            get { return _isDeadline; }
            set
            {
                _isDeadline = value;
                OnPropertyChanged(nameof(IsDeadline));
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

        public ICommand CreateTaskCommand { get; }
        public ICommand CloseCommand { get; }

        public CreateTaskViewModel(ITaskService taskService, ISessionService sessionService, INavigationService closeModalNavigationService)
        {
            _sessionService = sessionService;
            _taskService = taskService;
            _closeModalNavigationService = closeModalNavigationService;

            CreateTaskCommand = new ViewModelAsyncCommand(ExecuteCreateTask, CanExecuteCreateTask);
            CloseCommand = new ViewModelCommand(ExecuteClose);

            Deadline = DateTime.Now.AddDays(3);
        }

        private async Task ExecuteCreateTask(object parameters) 
        {
            try
            {
                var createTaskDto = new CreateTaskDto
                {
                    Title = Name,
                    Description = Description,
                    Deadline = IsDeadline ? Deadline : null,
                    Priority = int.Parse(Priority),
                    UserId = _sessionService.GetCurrentUserId()
                };

                var result = await _taskService.CreateTask(createTaskDto);
                if (result.Success)
                {
                    SuccessMessage = result.Message;
                    SharedDataStore.InvokeOnTaskMenuErrorMessageChange(this, new MessageEventArgs(result.Message, true));
                    SharedDataStore.InvokeOnTaskListChanged(this, new TaskDtoInFocusEventArgs(result.Data));
                    if (CloseCommand.CanExecute(parameters))
                    {
                        CloseCommand.Execute(parameters);
                    }
                }
                else
                {
                    ErrorMessage = result.Message;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to create task.";
            }
        }

        private bool CanExecuteCreateTask(object parameters) 
        {
            if (string.IsNullOrWhiteSpace(Name) ||  string.IsNullOrWhiteSpace(Priority) || !int.TryParse(Priority, out _))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteClose(object parameters)
        {
            _closeModalNavigationService.Navigate();
        }
    }
}
