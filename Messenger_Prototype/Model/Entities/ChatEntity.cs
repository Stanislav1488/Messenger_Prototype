using System;

namespace Messenger_Prototype.Model.Entities
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Contact { get; set; }
        public int LastMessage {  get; set; }
    }
}
