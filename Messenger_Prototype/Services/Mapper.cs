
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
                Id = entity.Id,
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
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Status = user.Status,
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

        public static Chat ToUiChat(ChatEntity chatEntity, UserEntity userEntity)
        {
            return new Chat
            {
                Id = chatEntity.Id,
                Partner = new Contact
                {
                    Login = userEntity.Login,
                    Name = userEntity.Name,
                    Status = userEntity.Status
                },
                Messages = new System.Collections.ObjectModel.ObservableCollection<Message>(),
                LastMessage = chatEntity.LastMessage
            };
        }
    }
}
