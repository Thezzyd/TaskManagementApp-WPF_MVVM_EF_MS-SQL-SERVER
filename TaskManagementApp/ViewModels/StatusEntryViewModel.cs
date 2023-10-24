
namespace TaskManagementApp.ViewModels
{
    public class StatusEntryViewModel : ViewModelBase
    {
        private Models.TaskStatus _status;
        private bool _isChecked;

        public Models.TaskStatus Status
        { 
            get { return _status; }
            set 
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

    }
}
