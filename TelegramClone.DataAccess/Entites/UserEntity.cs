using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.DataAccess.Entites
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfRegistration { get; set; } = DateTime.UtcNow;

        public string IPAddress { get; set; } = "0.0.0.0";
        public int Port { get; set; } = 0;
        public bool IsOnline { get; set; } = false;

        public List<ContactEntity> Contacts { get; set; } = new(); // Контакты пользователя
    }
}
