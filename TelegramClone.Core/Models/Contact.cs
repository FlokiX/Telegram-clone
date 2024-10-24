using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelegramClone.Core.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // Внешний ключ на User
        public string ContactUsername { get; set; } // Имя пользователя контакта
   
        // Связь один-к-одному с чатом
        public Guid ChatId { get; set; } // Внешний ключ на Chat
        
    }
}
