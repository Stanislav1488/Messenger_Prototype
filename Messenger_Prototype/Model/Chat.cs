using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype.Model
{
    public class Chat
    {
        public Contact Partner { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public string LastMessage { get; set; }
    }
}
