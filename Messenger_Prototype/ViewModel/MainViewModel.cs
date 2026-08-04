using Messenger_Prototype.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace Messenger_Prototype.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Chat> Chats { get; set; }

        private HubConnection _connection;
        private Chat _selectedChat;
        private string _messageText;
        private User _currentUser;
        public Chat selectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                OnPropertyChanged(nameof(selectedChat));
            }
        }


        public string messageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                OnPropertyChanged();
                (SendMessageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand SendMessageCommand { get; }

        public MainViewModel(User currentUser)
        {
            _currentUser = currentUser;
            Chats = new ObservableCollection<Chat>();

            List<User> allUsers = new List<User>
            {
                new User {Login = "admin", Name = "Админ"},
                new User {Login = "user", Name = "Stan"},
                new User {Login = "lina", Name = "Лина"},
                new User {Login = "egor", Name = "Егор"},
            };

            var contacts = allUsers.Where(u => u.Login != _currentUser.Login).ToList();

            foreach (var contact in contacts)
            {
                Contact contactModel = new Contact()
                {
                    Name = contact.Name,
                    Login = contact.Login,
                    Status = "online"
                };

                Chat chat = new Chat()
                {
                    Partner = contactModel,
                    Messages = new ObservableCollection<Message>()
                };

                Chats.Add(chat);
            }

            if(contacts.Count > 0)
            {
                selectedChat = Chats.First();
            }

            SendMessageCommand = new RelayCommand(SendMassage, CanSendMassage);
            Connect();
        }

        private async Task Connect()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub")
                .Build();
            _connection.On<string, string>("ReceiveMessage", (message, userName) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    bool isOwn = userName == _currentUser.Login;

                    Message newMessage = new Message
                    {
                        IsOwn = isOwn,
                        Text = message,
                        Timestamp = DateTime.Now
                    };

                    selectedChat.Messages.Add(newMessage);
                });
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("RegisterUser", _currentUser.Login);
        }

        private async void SendMassage(object parameter)
        {
            if (string.IsNullOrWhiteSpace(messageText))
            {
                return;
            }

            Message newMessage = new Message
            {
                IsOwn = true,
                Text = messageText,
                Timestamp = DateTime.Now
            };
            selectedChat.Messages.Add(newMessage);
            selectedChat.LastMessage = messageText;

            await _connection.InvokeAsync("Send", messageText, _currentUser.Login, selectedChat.Partner.Login);
            messageText = string.Empty;
        }

        private bool CanSendMassage(object parameter)
        {
            return !string.IsNullOrWhiteSpace(messageText);
        }
    }
}
