
using Messenger_Prototype.Model;
using Messenger_Prototype.Model.Entities;

namespace Messenger_Prototype.Services
{
    public static class Mapper
    {
        public static User ToUiUser(UserEntity entity)
        {
            return new User
            {
                Login = entity.Login,
                Name = entity.Name,
               Password = entity.Password,
               Status = entity.Status,
            };
        }

        public static UserEntity toEntityUser(User user)
        {
            return new UserEntity
            {
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Status = user.Status,
            };
        }

        public static Contact ToUiContact(ContactEntity entity)
        {
            return new Contact
            {
                AvatarPath = entity.AvatarPath,
                Login = entity.Login,
                Name= entity.Name,
                Status = entity.Status,
            };
        }

        public static ContactEntity toEntityContact(Contact contact)
        {
            return new ContactEntity
            {
                AvatarPath= contact.AvatarPath,
                Login= contact.Login,
                Name=contact.Name,
                Status= contact.Status,
            };
        }

        public static Message ToUiMessage(MessageEntity entity)
        {
            return new Message
            {
                IsOwn = entity.IsOwn,
                Text = entity.Text,
                Timestamp = entity.Timestamp,
            };
        }

        public static MessageEntity ToMessageEntity(Message message, int chatId)
        {
            return new MessageEntity
            {
                Text = message.Text,
                IsOwn = message.IsOwn,
                Timestamp = message.Timestamp,
                ChatId = chatId
            };
        }

        public static Chat ToUiChat(ChatEntity chatEntity, ContactEntity contactEntity)
        {
            return new Chat
            {
                Id = chatEntity.Id,
                Partner = ToUiContact(contactEntity),
                Messages = new System.Collections.ObjectModel.ObservableCollection<Message>(),
                LastMessage = chatEntity.LastMessage,
            };
        }
    }
}
