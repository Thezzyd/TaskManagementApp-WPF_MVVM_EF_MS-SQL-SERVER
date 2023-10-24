using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManagementApp.CustomControls
{
    public partial class CloseModalButton : UserControl
    {

        public ICommand SomeCommand
        {
            get { return (ICommand)GetValue(SomeCommandProperty); }
            set { SetValue(SomeCommandProperty, value); }
        }

        public static readonly DependencyProperty SomeCommandProperty =
            DependencyProperty.Register("SomeCommand", typeof(ICommand), typeof(CloseModalButton), new UIPropertyMetadata(null));

        public CloseModalButton()
        {
            InitializeComponent();
        }

    }
}
