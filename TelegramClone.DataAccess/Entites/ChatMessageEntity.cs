using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ChatMessageEntity
    {
        public Guid Id { get; set; } // Уникальный идентификатор сообщения
        public Guid ChatId { get; set; } // Внешний ключ на чат
        public string SenderUsername { get; set; } // Имя отправителя
        public string Content { get; set; } // Содержимое сообщения
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Время отправки сообщения

        // Связь с чатом
        public ChatEntity Chat { get; set; } // Чат, к которому относится сообщение
    }
}
