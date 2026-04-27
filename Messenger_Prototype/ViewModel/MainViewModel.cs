using Messenger_Prototype.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Messenger_Prototype.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Chat> Chats { get; set; }

        private Chat _selectedChat;
        public Chat selectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                OnPropertyChanged(nameof(selectedChat));
            }
        }

        private string _newMessageText;

        public string newMessageText
        {
            get { return _newMessageText; }
            set
            {
                _newMessageText = value;
                OnPropertyChanged();
                (SendMessageCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand SendMessageCommand { get; }

        public MainViewModel()
        {
            Chats = new ObservableCollection<Chat>();

            User user = new User { Name = "Линочка", Status = "online" };

            Chat chat = new Chat
            {
                Partner = user,
                Messages = new ObservableCollection<Message>()
            };

            Chats.Add(chat);
            selectedChat = chat;

            SendMessageCommand = new RelayCommand(SendMassage, CanSendMassage);
        }

        private void SendMassage(object parameter)
        {
            if (string.IsNullOrWhiteSpace(newMessageText))
            {
                return;
            }

            Message newMessage = new Message
            {
                IsOwn = true,
                Text = newMessageText,
                Timestamp = DateTime.Now
            };

            selectedChat.Messages.Add(newMessage);
            selectedChat.LastMessage = newMessageText;
            newMessageText = null;
        }

        private bool CanSendMassage(object parameter)
        {
            return !string.IsNullOrWhiteSpace(newMessageText);
        }
    }
}
