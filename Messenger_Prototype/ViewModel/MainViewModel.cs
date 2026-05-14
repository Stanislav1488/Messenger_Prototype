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

            User userFirst = new User { Name = "Линочка", Status = "online" };
            User userSecond = new User { Name = "Егор", Status = "last seen 10 minutes ago" };
            User userThird = new User { Name = "Ваня", Status = "last seen 1 hour ago" };

            Chat chatFirst = new Chat { Partner = userFirst, Messages = new ObservableCollection<Message>() };
            Chat chatSecond = new Chat { Partner = userSecond, Messages = new ObservableCollection<Message>() };
            Chat chatThird = new Chat { Partner = userThird, Messages = new ObservableCollection<Message>() };

            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "Привет, го секс с тобой, пряямо сейчас?" });
            chatFirst.Messages.Add(new Message { IsOwn = true, Text = "Привет, ЛИночка, да я жёстко тебя хочу" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "ну тогда ко мне приходи" });
            chatFirst.Messages.Add(new Message { IsOwn = false, Text = "в 16:00" });

            Chats.Add(chatSecond);
            Chats.Add(chatFirst);
            Chats.Add(chatThird);

            selectedChat = chatFirst;

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
