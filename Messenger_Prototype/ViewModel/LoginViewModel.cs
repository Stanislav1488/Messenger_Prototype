using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Messenger_Prototype.View;
using Messenger_Prototype.Model;
using System.Windows;

namespace Messenger_Prototype.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;
        private string _password;

        public string login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(login));
                (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(password));
                (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand LoginCommand { get; }

        private List<User> _users = new List<User>
        {
            new User { Login = "admin", Password = "123", Name = "Админ"},
            new User { Login = "user", Password = "123", Name = "Пользователь"}
        };

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        private void Login(object parameter)
        {
            if(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            User foundUser = _users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if(foundUser != null)
            {
                MessengerWindow chatWindow = new MessengerWindow();
                chatWindow.Title = $"Чат — {foundUser.Name}";

                MainViewModel mainVm = new MainViewModel(foundUser);
                chatWindow.DataContext = mainVm;

                chatWindow.Show();
                Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
            }
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(password);
        }
    }
}
