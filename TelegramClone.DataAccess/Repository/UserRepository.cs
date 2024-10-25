using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //СУКА ОТ КУДА БЕРЕТСЯ USERID В КОНТАКТЕ СДЕЛАТЬ ТАК ЧТОБЫ ОН ВСЕГДА БЫЛ ПРИ СОЗДАНИИ ЮЗЕРА
        public async Task<Contact> GetContactByUserIdAsync(Guid userId)
        {
            // Находим первый контакт, связанный с данным пользователем
            var contactEntity = await _context.Contacts
                .FirstOrDefaultAsync(c => c.UserId == userId); // Возвращаем один контакт или null

            // Проверка на null перед маппингом
            if (contactEntity == null)
                return null;

            // Возвращаем маппинг ContactEntity в Contact
            return MapToContact(contactEntity);
        }

        private Contact MapToContact(ContactEntity contactEntity)
        {
            // Создаем контакт с использованием метода Create
            var (contact, error) = Contact.Create(contactEntity.UserId, contactEntity.ContactUsername, contactEntity.ChatId);
            if (error != null)
            {
                // Здесь можно обработать ошибку, если это необходимо
                throw new Exception(error);
            }
            return contact;
        }

        /*public async Task<User> GetByIdAsync(Guid id)
        {

        }*/

        /*public async Task<IEnumerable<User>> GetAllAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(b => User.Create(b.Id, b.Username, b.Email, b.PasswordHash).user)
                .ToList();

            return users;
        }*/

        public async Task AddAsync(User user)
        {

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }


            var userEntity = new UserEntity
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                DateOfRegistration = user.DateOfRegistration,
                IPAddress = user.IPAddress,
                Port = user.Port,
                IsOnline = user.IsOnline

            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }




        public async Task<(User user, string error)> GetByEmailAsync(string email)
        {

            var userEntity = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (userEntity == null)
            {
                return (null, "User not found.");
            }
            var user = User.Create(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.PasswordHash).user;

            return (user, null);
        }




        /*public async Task UpdateAsync(Guid id, string username, string email, string passwordHash)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Username, u => username)
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.PasswordHash, u => passwordHash));

  
           await _context.SaveChangesAsync();
        }*/


        /*public async Task DeleteAsync(Guid id)
        {
        
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }

          
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
          
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }
            var user = User.Create(id, userEntity.Username, userEntity.Email, userEntity.PasswordHash).user;
            return user;
        }*/
    }


}

