using TaskManagementApp.GlobalCommands;
using TaskManagementApp.DTOs;
using TaskManagementApp.Extensions;
using TaskManagementApp.Services;
using TaskManagementApp.SharedData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagementApp.ViewModels
{
    public class TasksMenuViewModel :ViewModelBase
    {
        private ITaskService _taskService;
        private ISessionService _sessionService;

        private string _welcomeMessage;
        private string _searchTerm;
        private string _prioritySign;
        private string _priority;
        private ObservableCollection<StatusEntryViewModel> _statusItems;
        private bool _showVisible;
        private bool _showHidden;
        private Models.TaskStatus _statusUpdate;
        private bool _isHiddenUpdate;
        private string _description;
        private bool _isDescriptionVisible;
        private string _createdTime;
        private ObservableCollection<TaskDto> _userTasksToDisplay;
        private TaskDto _selectedUserTask;
        private string _errorMessage;
        private string _successMessage;
        private bool _isTasksDatagridEnabled;

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
            }
        }
        public IEnumerable<string> PrioritySignItems
        {
            get { return new[] { ">", "<" }; }
        }
        public string PrioritySign
        {
            get { return _prioritySign; }
            set
            {
                _prioritySign = value;
                OnPropertyChanged(nameof(PrioritySign));
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
        public ObservableCollection<StatusEntryViewModel> StatusEntryViewModelItems
        {
            get { return _statusItems; }
            set
            {
                _statusItems = value;
                OnPropertyChanged(nameof(StatusEntryViewModelItems));
            }
        }
        public bool ShowVisible
        {
            get { return _showVisible; }
            set
            {
                _showVisible = value;
                OnPropertyChanged(nameof(ShowVisible));
            }
        }
        public bool ShowHidden
        {
            get { return _showHidden; }
            set
            {
                _showHidden = value;
                OnPropertyChanged(nameof(ShowHidden));
            }
        }
        public Models.TaskStatus StatusUpdate
        {
            get { return _statusUpdate; }
            set
            {
                _statusUpdate = value;
                OnPropertyChanged(nameof(StatusUpdate));
            }
        }
        public bool IsHiddenUpdate
        {
            get { return _isHiddenUpdate; }
            set
            {
                _isHiddenUpdate = value;
                OnPropertyChanged(nameof(IsHiddenUpdate));
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
        public bool IsDescriptionVisible
        {
            get { return _isDescriptionVisible; }
            set
            {
                _isDescriptionVisible = value;
                OnPropertyChanged(nameof(IsDescriptionVisible));
            }
        }
        public string CreatedTime
        {
            get { return _createdTime; }
            set
            {
                _createdTime = "Created: " + value;
                OnPropertyChanged(nameof(CreatedTime));
            }
        }
        public ObservableCollection<TaskDto> UserTasksToDisplay
        {
            get { return _userTasksToDisplay; }
            set
            {
                _userTasksToDisplay = value;
                OnPropertyChanged(nameof(UserTasksToDisplay));
            }
        }
        private IEnumerable<TaskDto> UserTasks;
        public TaskDto SelectedUserTask
        {
            get { return _selectedUserTask; }
            set
            {
                _selectedUserTask = value;
                OnPropertyChanged(nameof(SelectedUserTask));
                if (SelectedUserTask != null)
                {
                    Description = SelectedUserTask.Description;
                    CreatedTime = SelectedUserTask.CreatedTime.ToLocalTime().ToString();
                    StatusUpdate = SelectedUserTask.Status;
                    IsHiddenUpdate = SelectedUserTask.IsHidden;
                    IsDescriptionVisible = true;
                }
                else
                {
                    IsDescriptionVisible = false;
                    Description = string.Empty;
                }
                SharedDataStore._currentlySelectedTaskDto = SelectedUserTask;
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
        public bool IsTasksDatagridEnabled
        {
            get { return _isTasksDatagridEnabled; }
            set
            {
                _isTasksDatagridEnabled = value;
                OnPropertyChanged(nameof(IsTasksDatagridEnabled));
            }
        }

        public ICommand NavigateToLoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand ResetAndApplyFiltersCommand { get; }
        public ICommand UpdateTaskStatusAndVisibilityCommand { get; }
        public ICommand CreateTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }

        public TasksMenuViewModel(ITaskService taskService ,ISessionService sessionService,
            INavigationService loginNavigationService, INavigationService createTaskNavigationService, INavigationService updateTaskNavigationService)
        {
            _taskService = taskService;
            _sessionService = sessionService;

            CreateTaskCommand = new ModalNavigateCommand(createTaskNavigationService, CanCreateTask);
            UpdateTaskCommand = new ModalNavigateCommand(updateTaskNavigationService, CanUpdateTask);
            NavigateToLoginCommand = new NavigateCommand(loginNavigationService);

            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand, CanExecuteLogoutCommand);
            ApplyFiltersCommand = new ViewModelCommand(ExecuteApplyFilters, CanExecuteApplyFilters);
            ResetAndApplyFiltersCommand = new ViewModelCommand(ExecuteResetAndApplayFilters, CanExecuteResetAndApplayFilters);
            UpdateTaskStatusAndVisibilityCommand = new ViewModelAsyncCommand(ExecuteUpdateTaskStatusAndVisibility, CanExecuteUpdateTaskStatusAndVisibility);
            RemoveTaskCommand = new ViewModelAsyncCommand(ExecuteRemoveTask, CanExecuteRemoveTask);

            SharedDataStore.OnTaskListChanged += SharedDataStore_OnTaskListChanged;
            SharedDataStore.OnTaskMenuResponseMessageChange += SharedDataSource_OnTaskMenuResponseMessageChange;
            SharedDataStore.OnModalWindowIsOpenedChange += SharedDataSource_OnModalWindowIsOpenedChange;

            DisplayCurrentUserData();
            InitializeStatusFilterOptions();

            IsTasksDatagridEnabled = true;
            PrioritySign = ">";
            SuccessMessage = "Logs, here you will see information about performed actions.";
        }

        private void SharedDataSource_OnModalWindowIsOpenedChange(object sender, bool e_isOpen)
        {
            if (e_isOpen)
            {
                IsTasksDatagridEnabled = false;
            }
            else
            {
                IsTasksDatagridEnabled = true;
            }
        }

        private void SharedDataStore_OnTaskListChanged(object sender, TaskDtoInFocusEventArgs e)
        {
            if (e != null)
            {
                LoadAllUserTasks(false, e.TaskDtoData.Id).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        ErrorMessage = "Failed to load user tasks.";
                    }
                });

            }
            else
            { 
                LoadAllUserTasks(false).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        ErrorMessage = "Failed to load user tasks.";
                    }
                });
            }
        }

        private void SharedDataSource_OnTaskMenuResponseMessageChange(object sender, MessageEventArgs e)
        {
            if (e.Success)
            {
                SuccessMessage = e.Message;
            }
            else
            {
                ErrorMessage = e.Message;
            }
        }

        private void InitializeStatusFilterOptions()
        {
            StatusEntryViewModelItems = new ObservableCollection<StatusEntryViewModel>();
            foreach (var status in Enum.GetValues(typeof(Models.TaskStatus)).Cast<Models.TaskStatus>())
            {
                StatusEntryViewModelItems.Add(new StatusEntryViewModel { Status = status });
            }
        }

        private bool CanCreateTask(object parameter)
        {
            if (SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        private bool CanUpdateTask(object parameter)
        {
            if (SelectedUserTask == null || SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CanExecuteRemoveTask(object parameter)
        {
            if (SelectedUserTask != null && !SharedDataStore.IsModalWindowOpened)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task ExecuteRemoveTask(object parameter)
        {
            try
            {
                var result = await _taskService.RemoveTask(SelectedUserTask.Id);
                if (result.Success)
                { 
                    SuccessMessage = result.Message;
                    SharedDataStore.InvokeOnTaskListChanged(this);
                }
                else
                {
                    ErrorMessage = result.Message;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to remove task.";
            }
        }

        private bool CanExecuteUpdateTaskStatusAndVisibility(object parameter)
        {
            if (SelectedUserTask == null || SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else if (StatusUpdate == SelectedUserTask.Status && IsHiddenUpdate == SelectedUserTask.IsHidden)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task ExecuteUpdateTaskStatusAndVisibility(object parameter)
        {
            try
            {
                var result = await _taskService.UpdateTaskStatusAndVisibility(SelectedUserTask.Id, StatusUpdate, IsHiddenUpdate);
                if (result.Success)
                {
                    SuccessMessage = result.Message;
                    SharedDataStore.InvokeOnTaskListChanged(this, new TaskDtoInFocusEventArgs(result.Data));
                }
                else
                {
                    ErrorMessage = result.Message;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Faliled to update status and visibility.";
            }
        }

        private void DisplayCurrentUserData()
        {
            string username = _sessionService.GetCurrentUserUsername();
            if (username != null)
            {
                WelcomeMessage = "Welcome " + username + "!";
            }
            else
            {
                if (LogoutCommand.CanExecute(null))
                {
                    LogoutCommand.Execute(null);
                }
            }
        }

        private bool CanExecuteLogoutCommand(object parameter)
        {
            if (SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteLogoutCommand(object parameter)
        {
            if (NavigateToLoginCommand.CanExecute(parameter))
            {
                _sessionService.SetCurrentUser(null);
                SharedDataStore.OnTaskListChanged -= SharedDataStore_OnTaskListChanged;
                SharedDataStore.OnTaskMenuResponseMessageChange -= SharedDataSource_OnTaskMenuResponseMessageChange;
                SharedDataStore.OnModalWindowIsOpenedChange -= SharedDataSource_OnModalWindowIsOpenedChange;

                NavigateToLoginCommand.Execute(parameter);
            }
        }

        private async Task LoadAllUserTasks(bool resetFilters, Guid idTaskToSelect = new Guid())
        {
            try
            {
                var result = await _taskService.GetAllUserTasks(_sessionService.GetCurrentUserId());
                if (result.Success)
                {
                    UserTasks = new ObservableCollection<TaskDto>(result.Data);
                    if (resetFilters)
                    {
                        if (ResetAndApplyFiltersCommand.CanExecute(null))
                        {
                            ResetAndApplyFiltersCommand.Execute(null);
                        }
                    }
                    else 
                    {
                        if (ApplyFiltersCommand.CanExecute(null))
                        {
                            ApplyFiltersCommand.Execute(null);
                        }
                    }

                    if (idTaskToSelect != Guid.Empty)
                    {
                        SelectedUserTask = UserTasks.FirstOrDefault(t => t.Id == idTaskToSelect);
                    }
                }
                else
                {
                    ErrorMessage = result.Message;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to load all user tasks.";
            }
        }

        private bool CanExecuteApplyFilters(object parameter)
        {
            if (SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteApplyFilters(object parameters) {
            try 
            {
                UserTasksToDisplay = new ObservableCollection<TaskDto>();
                var filteredTasks = new ObservableCollection<TaskDto>(UserTasks
                                                .Search(SearchTerm)
                                                .FilterPriority(Priority, PrioritySign)
                                                .FilterStatus(StatusEntryViewModelItems)
                                                .FilterVisibility(ShowVisible, ShowHidden)
                                                .OrderByDescending(t => t.Priority));

                foreach (var task in filteredTasks)
                {
                    UserTasksToDisplay.Add(task);
                }
            }
            catch (Exception) 
            {
                ErrorMessage = "Failed to apply filters";
            }
           
        }

        private bool CanExecuteResetAndApplayFilters(object parameter)
        {
            if (SharedDataStore.IsModalWindowOpened)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ExecuteResetAndApplayFilters(object parameter)
        {
            try
            {
                SearchTerm = string.Empty;
                Priority = string.Empty;
                ShowVisible = true;
                ShowHidden = false;
                foreach (var status in StatusEntryViewModelItems)
                {
                    status.IsChecked = true;
                }

                if (ApplyFiltersCommand.CanExecute(parameter))
                {
                    ApplyFiltersCommand.Execute(parameter);
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to reset and apply filters";
            }
        }

       public static TasksMenuViewModel LoadTasksMenuViewModel(ITaskService taskService,
           ISessionService sessionService, NavigationService loginNavigationService, ModalNavigationService createTaskNavigationService,
           ModalNavigationService updateTaskNavigationService)
       {
            TasksMenuViewModel tasksMenuViewModel = new TasksMenuViewModel(taskService, sessionService, loginNavigationService, createTaskNavigationService, updateTaskNavigationService);
            tasksMenuViewModel.DisplayCurrentUserData();
            tasksMenuViewModel.LoadAllUserTasks(true).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    tasksMenuViewModel.ErrorMessage = "Failed to load tasks.";
                }
            });
            return tasksMenuViewModel;
       }
    }
}
