using Messenger_Prototype.ViewModel;
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
using System.Windows.Shapes;

namespace Messenger_Prototype.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if(passwordTologin != null)
            {
                TextBlock hint = passwordTologin.Template.FindName("hint", passwordTologin) as TextBlock;

                if(hint != null)
                {
                    if (passwordTologin.Password.Length > 0)
                    {
                        hint.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        hint.Visibility = Visibility.Visible;
                    }
                }

                if (DataContext is LoginViewModel vm)
                {
                    vm.password = passwordTologin.Password;
                }
            }
        }
    }
}
