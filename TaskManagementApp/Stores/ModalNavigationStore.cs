using TaskManagementApp.SharedData;
using TaskManagementApp.ViewModels;
using System;

namespace TaskManagementApp.Stores
{
    public class ModalNavigationStore
    {
        public event Action CurrentViewModelChanged;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                
                SharedDataStore.IsModalWindowOpened = _currentViewModel != null;
                OnCurrentViewModelChange();
            }
        }

        public bool IsOpen => _currentViewModel != null;
        public void Close()
        {
            CurrentViewModel = null;
        }
        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
