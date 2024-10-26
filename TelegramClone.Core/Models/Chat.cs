using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
    public class Chat
    {
        public Guid Id { get; } = Guid.NewGuid(); // Уникальный идентификатор чата
        public Guid User1Id { get; } // ID первого пользователя
        public Guid User2Id { get; } // ID второго пользователя

        private List<ChatMessage> messages = new(); // Сообщения в чате
        public IReadOnlyList<ChatMessage> Messages => messages.AsReadOnly(); // Чтение сообщений

        // Приватный конструктор для создания чата
        private Chat(Guid user1Id, Guid user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }

        // Статический метод для создания чата
        public static (Chat chat, string error) Create(Guid user1Id, Guid user2Id)
        {
            if (user1Id == user2Id)
            {
                return (null, "Users cannot be the same."); // Ошибка, если пользователи одинаковые
            }

            var newChat = new Chat(user1Id, user2Id);
            return (newChat, null);
        }

        private Chat(Guid id, Guid user1Id, Guid user2Id, List<ChatMessage> messages)
        {
            Id = id;
            User1Id = user1Id;
            User2Id = user2Id;
            this.messages = messages;
        }

        // Статический метод для создания чата
        public static Chat CreateChat(Guid id, Guid user1Id, Guid user2Id, List<ChatMessage> messages)
        {
            return new Chat(id, user1Id, user2Id, messages);
        }



       
        public (ChatMessage message, string error) SendMessage(string senderUsername, string content)
        {
            var (message, error) = ChatMessage.Create(this.Id, senderUsername, content);
            if (message != null)
            {
                messages.Add(message); // Добавляем сообщение в список
            }
            return (message, error);
        }

 

    }

}
