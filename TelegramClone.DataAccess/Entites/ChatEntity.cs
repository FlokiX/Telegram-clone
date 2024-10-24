using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ChatEntity
    {
        public Guid Id { get; set; } // Уникальный идентификатор чата
        public string Name { get; set; } // Имя чата
        public List<ChatMessageEntity> Messages { get; set; } = new(); // Сообщения в чате
        public List<Guid> UserIds { get; set; } = new(); // Пользователи, входящие в чат
    }
}
