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

            Contact contactFirst = new Contact { Name = "Линочка", Status = "online" };
            Contact contactSecond = new Contact { Name = "Егор", Status = "last seen 10 minutes ago" };
            Contact contactThird = new Contact { Name = "Ваня", Status = "last seen 1 hour ago" };

            Chat chatFirst = new Chat { Partner = contactFirst, Messages = new ObservableCollection<Message>() };
            Chat chatSecond = new Chat { Partner = contactSecond, Messages = new ObservableCollection<Message>() };
            Chat chatThird = new Chat { Partner = contactThird, Messages = new ObservableCollection<Message>() };

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
