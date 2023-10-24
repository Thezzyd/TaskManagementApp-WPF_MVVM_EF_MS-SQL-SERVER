using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManagementApp.CustomControls
{
    /// <summary>
    /// Interaction logic for LogoutButton.xaml
    /// </summary>
    public partial class LogoutButton : UserControl
    {

        public static readonly DependencyProperty SomeCommandProperty = DependencyProperty.Register("SomeCommand", typeof(ICommand), typeof(LogoutButton), new UIPropertyMetadata(null));

        public ICommand SomeCommand
        {
            get { return (ICommand)GetValue(SomeCommandProperty); }
            set { SetValue(SomeCommandProperty, value); }
        }

        public LogoutButton()
        {
            InitializeComponent();
        }
    }
}
