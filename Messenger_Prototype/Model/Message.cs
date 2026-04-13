using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype.Model
{
    public class Message
    {
        public DateTime Timestamp { get; set; }
        public string Text { get; set; }
        public bool IsOwn { get; set; }
    }
}
