using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger_Prototype.Model;

namespace Messenger_Prototype.ViewModel
{
    internal class  MainViewModel : BaseViewModel
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

        public MainViewModel()
        {
            Chats = new ObservableCollection<Chat>();

            User user = new User { Name = "Линочка", Status = "online"};

            Chat chat = new Chat { Partner = user};

            Chats.Add(chat);
        }
    }
}
