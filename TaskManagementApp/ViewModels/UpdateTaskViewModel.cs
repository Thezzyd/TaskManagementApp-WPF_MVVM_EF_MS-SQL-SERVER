

using TaskManagementApp.DTOs;
using TaskManagementApp.Services;
using TaskManagementApp.SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagementApp.ViewModels
{
    public class UpdateTaskViewModel : ViewModelBase
    {
        private readonly ITaskService _taskService;
        private readonly INavigationService _closeModalNavigationService;

        private string _name;
        private string _description;
        private DateTime? _deadline;
        private bool _isDeadline;
        private string _priority;
        private Models.TaskStatus _status;
        private bool _isHidden;
        private string _errorMessage;
        private string _successMessage;

        private TaskDto _selectedTaskDtoToUpdate;

        public string Name
        {
            get { return _name; }
            set {
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
        public bool IsDeadline
        {
            get { return _isDeadline; }
            set
            {
                _isDeadline = value;
                OnPropertyChanged(nameof(IsDeadline));
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
        public Models.TaskStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                _isHidden = value;
                OnPropertyChanged(nameof(IsHidden));
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
        public TaskDto SelectedTaskDtoToUpdate
        {
            get { return _selectedTaskDtoToUpdate; }
            set
            {
                _selectedTaskDtoToUpdate = value;
                OnPropertyChanged(nameof(SelectedTaskDtoToUpdate));
            }
        }

        public ICommand UpdateTaskCommand { get; }
        public ICommand CloseCommand { get; }

        public UpdateTaskViewModel(ITaskService taskService, INavigationService closeModalNavigationService)
        {
            _taskService = taskService;
            _closeModalNavigationService = closeModalNavigationService;

            UpdateTaskCommand = new ViewModelAsyncCommand(ExecuteUpdateTask, CanExecuteUpdateTask);
            CloseCommand = new ViewModelCommand(ExecuteClose);

           
            LoadDataOfTaskToUpdate();
        }

        private void LoadDataOfTaskToUpdate()
        {
            SelectedTaskDtoToUpdate = SharedDataStore._currentlySelectedTaskDto;

            Name = SelectedTaskDtoToUpdate.Name;
            Description = SelectedTaskDtoToUpdate.Description;
            Deadline = SelectedTaskDtoToUpdate.Deadline;
            if (Deadline != null)
            {
                IsDeadline = true;
            }
            Priority = SelectedTaskDtoToUpdate.Priority.ToString();
            Status = SelectedTaskDtoToUpdate.Status;
            IsHidden = SelectedTaskDtoToUpdate.IsHidden;
        }

        private void ExecuteClose(object obj)
        {
            _closeModalNavigationService.Navigate();
        }

        private bool CanExecuteUpdateTask(object obj)
        {
            if (SelectedTaskDtoToUpdate == null)
            {
                return false;
            }
            if (SelectedTaskDtoToUpdate.Name == Name &&
               SelectedTaskDtoToUpdate.Description == Description &&
               SelectedTaskDtoToUpdate.Deadline == Deadline &&
               SelectedTaskDtoToUpdate.Priority.ToString() == Priority &&
               SelectedTaskDtoToUpdate.Status == Status &&
               SelectedTaskDtoToUpdate.IsHidden == IsHidden)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Priority) || !Int32.TryParse(Priority, out _))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task ExecuteUpdateTask(object parameters)
        {
            try
            {
                var updateTaskDto = new TaskDto
                {
                    Id = SelectedTaskDtoToUpdate.Id,
                    Name = Name,
                    Description = Description,
                    Deadline = Deadline,
                    Priority = Int32.Parse(Priority),
                    IsHidden = IsHidden,
                    Status = Status
                };

                var result = await _taskService.UpdateTask(updateTaskDto);
                if(result.Success) 
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

            }catch (Exception)
            {
                ErrorMessage = "Failed to update task.";
            }
        }

    }
}
