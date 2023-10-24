using TaskManagementApp.Services;
using System;

namespace TaskManagementApp.GlobalCommands
{
    public class ModalNavigateCommand : NavigateCommand
    {
        public ModalNavigateCommand(INavigationService navigationService) : base(navigationService)
        {
        }

        public ModalNavigateCommand(INavigationService navigationService, Predicate<object> canExecuteAction) : base(navigationService, canExecuteAction)
        {
        }
    }
}
