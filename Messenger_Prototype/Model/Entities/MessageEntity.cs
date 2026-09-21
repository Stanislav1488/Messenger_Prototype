using System;

namespace Messenger_Prototype.Model.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsOwn { get; set; }
        public int ChatId { get; set; }
    }
}
