using Messenger_Prototype.Data;
using Messenger_Prototype.Model;
using Messenger_Prototype.Model.Entities;
using Messenger_Prototype.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
        private IProfileUser _selectedProfileUser;
        private bool _isOpenProfile;

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
        public IProfileUser selectedProfileUser
        {
            get { return _selectedProfileUser; }
            set
            {
                _selectedProfileUser = value;
                OnPropertyChanged(nameof(selectedProfileUser));
            }
        }
        public bool isOpenProfile
        {
            get { return _isOpenProfile; }
            set
            {
                _isOpenProfile = value;
                OnPropertyChanged(nameof(isOpenProfile));
            }
        }

        public ICommand SendMessageCommand { get; }
        public ICommand OpenMyProfileCommand { get; }
        public ICommand OpenPartnerProfileCommand { get; }
        public ICommand CloseProfileCommand { get; }

        public MainViewModel(User currentUser)
        {
            _currentUser = currentUser;
            Chats = new ObservableCollection<Chat>();

            using var db = new AppDbContext();

            var chatsFromDb = db.Chats.Where(u => u.UserId == currentUser.Id).ToList();

            if (chatsFromDb.Count == 0)
            {
                var allUsers = db.Users.Where(u => u.Login != _currentUser.Login).ToList();

                foreach (var userEntity in allUsers)
                {
                    var newChatEntity = new ChatEntity
                    {
                        UserId = currentUser.Id,
                        ContactId = userEntity.Id,
                        LastMessage = ""
                    };
                    db.Chats.Add(newChatEntity);
                    db.SaveChanges();

                    chatsFromDb.Add(newChatEntity);
                }
            }

            foreach (var chatEntity in chatsFromDb)
            {
                var userEntity = db.Users.FirstOrDefault(c => c.Id == chatEntity.ContactId);

                if (userEntity == null) continue;

                Contact contactModel = new Contact
                {
                    AvatarPath = "",
                    Login = userEntity.Login,
                    Name = userEntity.Name,
                    Status = userEntity.Status
                };

                Chat chat = new Chat
                {
                    Id = chatEntity.Id,
                    LastMessage = "",
                    Partner = contactModel,
                    Messages = new ObservableCollection<Message>()
                };
                
                Chats.Add(chat);
            }

            if (Chats.Count > 0)
            {
                selectedChat = Chats.First();
            }

            OpenMyProfileCommand = new RelayCommand(OpenMyProfile);
            OpenPartnerProfileCommand = new RelayCommand(OpenPartnerProfile);
            CloseProfileCommand = new RelayCommand(CloseProfile);
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);

            Connect();
        }
        public event Action ScrollBottom;

        public void OnScrollBottom()
        {
            ScrollBottom?.Invoke();
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
                    selectedChat.LastMessage = message;
                    selectedChat.LastMessageTime = newMessage.Timestamp.ToString("HH:mm");
                    OnScrollBottom();
                });
            });
            _connection.On<List<string>>("UpdateOnlineUsers", (users) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var chat in Chats)
                    {
                        if (users.Contains(chat.Partner.Login))
                        {
                            chat.Partner.Status = "online";
                        }
                        else
                        {
                            chat.Partner.Status = "offline";
                        }
                    }

                    if (_currentUser != null)
                    {
                        if (users.Contains(_currentUser.Login))
                            _currentUser.Status = "online";
                        else
                            _currentUser.Status = "offline";
                    }
                });
            });

            await _connection.StartAsync();
            await _connection.InvokeAsync("RegisterUser", _currentUser.Login);
            await _connection.InvokeAsync("UserConnected", _currentUser.Login);
        }

        private async void SendMessage(object parameter)
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

            using var db = new AppDbContext();
            var messageEntity = new MessageEntity
            {
                Text = messageText,
                UserName = _currentUser.Name,
                IsOwn = true,
                Timestamp = DateTime.Now,
                ChatId = selectedChat.Id
            };
            db.Messages.Add(messageEntity);
            db.SaveChanges();

            selectedChat.Messages.Add(newMessage);
            selectedChat.LastMessage = messageText;
            selectedChat.LastMessageTime = newMessage.Timestamp.ToString("HH:mm");

            OnScrollBottom();

            await _connection.InvokeAsync("Send", messageText, _currentUser.Login, selectedChat.Partner.Login);
            messageText = string.Empty;
        }

        private void OpenMyProfile(object parameter)
        {
            selectedProfileUser = _currentUser;
            isOpenProfile = true;
        }

        private void OpenPartnerProfile(object parameter)
        {
            selectedProfileUser = selectedChat.Partner;
            isOpenProfile = true;
        }

        private void CloseProfile(object parameter)
        {
            isOpenProfile = false;
            selectedProfileUser = null;
        }

        private bool CanSendMessage(object parameter)
        {
            return !string.IsNullOrWhiteSpace(messageText);
        }

        //срабатывание события обновления статуса на офлайн
        //public async Task SetOfflineStatus()
        //{
        //    if (_connection != null && _connection.State == HubConnectionState.Connected)
        //    {
        //        await _connection.InvokeAsync("UserDisconnected", _currentUser.Login);
        //    }
        //}
    }
}
