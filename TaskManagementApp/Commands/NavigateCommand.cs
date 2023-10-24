using TaskManagementApp.Services;
using TaskManagementApp.Stores;
using TaskManagementApp.ViewModels;
using TaskManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly Predicate<object> _canExecuteAction;

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
