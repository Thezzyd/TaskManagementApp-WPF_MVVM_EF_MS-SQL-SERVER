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
    /// Interaction logic for CustomStandardButton.xaml
    /// </summary>
    public partial class CustomStandardButton : UserControl
    {
        public ICommand SomeCommand
        {
            get { return (ICommand)GetValue(SomeCommandProperty); }
            set { SetValue(SomeCommandProperty, value); }
        }

        public static readonly DependencyProperty SomeCommandProperty =
            DependencyProperty.Register("SomeCommand", typeof(ICommand), typeof(CustomStandardButton), new UIPropertyMetadata(null));

        public string SomeContent
        {
            get { return (string)GetValue(SomeContentProperty); }
            set { SetValue(SomeContentProperty, value); }
        }

        public static readonly DependencyProperty SomeContentProperty =
            DependencyProperty.Register("SomeContent", typeof(string), typeof(CustomStandardButton), new UIPropertyMetadata(null));


        public CustomStandardButton()
        {
            InitializeComponent();
        }
    }
}
