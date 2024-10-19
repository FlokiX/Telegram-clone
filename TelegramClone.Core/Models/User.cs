
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramClone.Core.Models
{
   

    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Username { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public DateTime DateOfRegistration { get; }

        private User(Guid Id, string username, string email, string password, DateTime dateOfRegistration)
        {
            Username = username;
            Email = email;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            DateOfRegistration = dateOfRegistration;
        }

        public static (User user, string error) Create(Guid Id, string username, string email, string password)
        {
         
            if (string.IsNullOrWhiteSpace(username) || username.Length > 20)
            {
                return (null, "Username cannot be null or empty and must be less than or equal to 20 characters.");
            }
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                return (null, "Email must be valid and end with 'gmail.com'.");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return (null, "Password must be at least 8 characters long.");
            }
            DateTime now = DateTime.UtcNow;

           
            var newUser = new User(Id, username, email, password, DateTime.UtcNow);
            return (newUser, null);

        }


        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        private static bool IsValidEmail(string email)
        {
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }
    }


}
