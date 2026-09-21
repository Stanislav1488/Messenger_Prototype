using Microsoft.EntityFrameworkCore.Query.Internal;
using System;

namespace Messenger_Prototype.Model.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Status { get; set; }
        public string AvatarPath {  get; set; }
    }
}
