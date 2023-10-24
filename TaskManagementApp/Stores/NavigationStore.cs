using TaskManagementApp.ViewModels;
using System;

namespace TaskManagementApp.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChange();
            }
        }

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
