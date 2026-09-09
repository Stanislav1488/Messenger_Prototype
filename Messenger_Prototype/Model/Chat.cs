using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messenger_Prototype.ViewModel;

namespace Messenger_Prototype.Model
{
    public class Chat : BaseViewModel
    {
        private string _lastMessage;
        private string _lastMessageTime;
        public Contact Partner { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public string LastMessage
        {
            get { return _lastMessage; }
            set
            {
                _lastMessage = value;
                OnPropertyChanged(nameof(LastMessage));
            }
        }
        public string LastMessageTime
        {
            get => _lastMessageTime;
            set
            {
                _lastMessageTime = value;
                OnPropertyChanged(nameof(LastMessageTime));
            }
        }
    }
}
