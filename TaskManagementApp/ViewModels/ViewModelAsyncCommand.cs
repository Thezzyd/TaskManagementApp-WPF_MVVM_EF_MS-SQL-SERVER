using TaskManagementApp.SharedData;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManagementApp.ViewModels
{
    public class ViewModelAsyncCommand : ICommand
    {
        private readonly Func<object, Task> _executeAction;
        private readonly Predicate<object> _canExecuteAction;
        private bool _isExecuting;
     
        public ViewModelAsyncCommand(Func<object, Task> executeAction, Predicate<object> canExecuteAction = null)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecuteAction == null ? true : _canExecuteAction(parameter));
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _executeAction(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync(parameter).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    SharedDataStore.InvokeOnTaskMenuErrorMessageChange(this, new MessageEventArgs("Failed to execute async command", false));
                }
            });
        }
    }
}
