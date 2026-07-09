using Messenger_Prototype.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger_Prototype.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Chat> Chats { get; set; }

        private HubConnection _connection;
        private Chat _selectedChat;
        private string _messageText;
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

        public MainViewModel()
        {
            Chats = new ObservableCollection<Chat>();

            User userFirst = new User { Name = "Линочка", Status = "online" };
            User userSecond = new User { Name = "Егор", Status = "last seen 10 minutes ago" };
            User userThird = new User { Name = "Ваня", Status = "last seen 1 hour ago" };

            Chat chatFirst = new Chat { Partner = userFirst, Messages = new ObservableCollection<Message>() };
            Chat chatSecond = new Chat { Partner = userSecond, Messages = new ObservableCollection<Message>() };
            Chat chatThird = new Chat { Partner = userThird, Messages = new ObservableCollection<Message>() };

            Chats.Add(chatSecond);
            Chats.Add(chatFirst);
            Chats.Add(chatThird);

            selectedChat = chatFirst;

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
                    Message newMessage = new Message
                    {
                        IsOwn = userName == "You",
                        Text = message,
                        Timestamp = DateTime.Now
                    };

                    selectedChat.Messages.Add(newMessage);
                });
            });

            await _connection.StartAsync();
        }

        private async void SendMassage(object parameter)
        {
            if (string.IsNullOrWhiteSpace(messageText))
            {
                return;
            }

            await _connection.InvokeAsync("Send", messageText, "You");
            selectedChat.LastMessage = messageText;
            messageText = string.Empty;
        }

        private bool CanSendMassage(object parameter)
        {
            return !string.IsNullOrWhiteSpace(messageText);
        }
    }
}
