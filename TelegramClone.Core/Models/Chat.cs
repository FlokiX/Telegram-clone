using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
    public class Chat
    {
        public Guid Id { get; } = Guid.NewGuid(); 
        public string Name { get; }

        private List<ChatMessage> messages = new(); // Сообщения в чате
        public IReadOnlyList<ChatMessage> Messages => messages.AsReadOnly();

        private List<Guid> userIds = new(); // Пользователи, входящие в чат
        public IReadOnlyList<Guid> UserIds => userIds.AsReadOnly(); 

        private Chat(string name)
        {
            Name = name;
        }

        // Статический метод для создания чата
        public static (Chat chat, string error) Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            {
                return (null, "Chat name cannot be null or empty and must be less than or equal to 50 characters.");
            }

            var newChat = new Chat(name);
            return (newChat, null);
        }

        // Метод для добавления сообщения в чат
        public (ChatMessage message, string error) SendMessage(string senderUsername, string content)
        {
            var (message, error) = ChatMessage.Create(this.Id, senderUsername, content);
            if (message != null)
            {
                messages.Add(message); 
            }
            return (message, error);
        }

        // Метод для добавления пользователя в чат
        public string AddUser(Guid userId)
        {
            if (!userIds.Contains(userId))
            {
                userIds.Add(userId); 
            }
            return null;
        }
    }

}
