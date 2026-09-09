using Messenger_Prototype.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype.Model
{
    public class Message : BaseViewModel
    {
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get {  return timestamp; }
            set
            {
                timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
            }
        }
        public string Text { get; set; }
        public bool IsOwn { get; set; }
    }
}
