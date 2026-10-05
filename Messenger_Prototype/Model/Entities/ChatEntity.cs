using System;

namespace Messenger_Prototype.Model.Entities
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LastMessage {  get; set; }
        public int ContactId { get; internal set; }
    }
}
