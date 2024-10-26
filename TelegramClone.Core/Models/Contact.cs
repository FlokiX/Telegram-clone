using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelegramClone.Core.Models
{
    public class Contact
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Guid UserId { get; } 
        public string ContactUsername { get; }

        public Guid ChatId { get; }

        // Конструктор для создания контакта
        private Contact(Guid userId, string contactUsername)
        {
            UserId = userId;
            ContactUsername = contactUsername;
        }

        // Статический метод для создания контакта
        public static (Contact contact, string error) Create(Guid userId, string contactUsername)
        {
            if (string.IsNullOrWhiteSpace(contactUsername) || contactUsername.Length > 20)
            {
                return (null, "Contact username cannot be null or empty and must be less than or equal to 20 characters.");
            }

            var newContact = new Contact(userId, contactUsername);
            return (newContact, null);
        }

    }

}
