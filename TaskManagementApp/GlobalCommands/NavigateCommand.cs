using TaskManagementApp.Services;
using System;

namespace TaskManagementApp.GlobalCommands
{
    public class NavigateCommand : CommandBase
    {
        protected readonly INavigationService _navigationService;
        protected readonly Predicate<object> _canExecuteAction;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _canExecuteAction = null;
        }

        public NavigateCommand(INavigationService navigationService, Predicate<object> canExecuteAction)
        {
            _navigationService = navigationService;
            _canExecuteAction = canExecuteAction;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }
    }
}
