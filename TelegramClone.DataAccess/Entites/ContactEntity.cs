using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.DataAccess.Entites
{
    public class ContactEntity
    {
        public Guid Id { get; set; } // Уникальный идентификатор контакта
        public Guid UserId { get; set; } // Внешний ключ на пользователя
        public string ContactUsername { get; set; } // Имя пользователя контакта

        // Связь один-к-одному с чатом
        public Guid ChatId { get; set; } // Внешний ключ на чат
        public ChatEntity Chat { get; set; } // Чат, связанный с контактом

        // Связь с пользователем
        public UserEntity User { get; set; } // Навигационное свойство на пользователя
    }
}
