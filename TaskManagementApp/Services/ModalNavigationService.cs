using TaskManagementApp.Stores;
using TaskManagementApp.ViewModels;
using System;

namespace TaskManagementApp.Services
{
    public class ModalNavigationService : INavigationService
    {
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public ModalNavigationService(ModalNavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _createViewModel = createViewModel;
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
