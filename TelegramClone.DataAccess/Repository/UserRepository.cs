using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions;
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

        /*public async Task<User> GetByIdAsync(Guid id)
        {

        }*/

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(b => User.Create(b.Id, b.Username, b.Email, b.PasswordHash).user)
                .ToList();

            return users;
        }

        public async Task AddAsync(User user)
        {
          
            var userEntity = new UserEntity
            {
                Id = Guid.NewGuid(), //новый id
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash, 
                DateOfRegistration = DateTime.UtcNow
            };

            // Добавление UserEntity в контекст
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string username, string email, string passwordHash)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Username, u => username)
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.PasswordHash, u => passwordHash));

  
           await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
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
        }
    }


}

