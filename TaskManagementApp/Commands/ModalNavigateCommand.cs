using TaskManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Commands
{
    public class ModalNavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly Predicate<object> _canExecuteCommand;

        public ModalNavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ModalNavigateCommand(INavigationService navigationService, Predicate<object> canExecuteCommand)
        {
            _navigationService = navigationService;
            _canExecuteCommand = canExecuteCommand;
        }

        public override void Execute(object parameter)
        {
            
            _navigationService.Navigate();
        }

        public override bool CanExecute(object parameter)
        {

            return _canExecuteCommand == null ? true : _canExecuteCommand(parameter);
        }
    }
}
