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
        public Guid User1Id { get; set; } // ID первого пользователя
        public Guid User2Id { get; set; } // ID второго пользователя
        public List<ChatMessageEntity> Messages { get; set; } = new(); // Сообщения в чате
    }
}
