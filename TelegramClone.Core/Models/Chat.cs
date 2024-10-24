using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public List<ChatMessage> Messages { get; set; } = new(); // Сообщения в чате
        public List<Guid> UserIds { get; set; } = new(); // Пользователи, входящие в чат
    }
}
