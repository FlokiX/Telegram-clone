using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
    public class ChatMessage
    {
        public Guid Id { get; } = Guid.NewGuid(); // Уникальный идентификатор сообщения
        public Guid ChatId { get; }
        public string SenderUsername { get; }
        public string Content { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow; 

        private ChatMessage(Guid chatId, string senderUsername, string content)
        {
            ChatId = chatId;
            SenderUsername = senderUsername;
            Content = content;
        }

 
        public static (ChatMessage message, string error) Create(Guid chatId, string senderUsername, string content)
        {
            
            if (string.IsNullOrWhiteSpace(senderUsername) || senderUsername.Length > 20)
            {
                return (null, "Sender username cannot be null or empty and must be less than or equal to 20 characters.");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return (null, "Message content cannot be null or empty.");
            }

          
            var newMessage = new ChatMessage(chatId, senderUsername, content);
            return (newMessage, null); // Успех
        }
    }

}
