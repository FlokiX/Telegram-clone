using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions;
using TelegramClone.Core.Models;

namespace TelegramClone.Application.Services
{
    
    namespace TelegramClone.Services
    {
       
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<IEnumerable<User>> GetAllAsync()
            {
                return await _userRepository.GetAllAsync();
            }

            public async Task<User> GetByIdAsync(Guid id)
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new Exception($"User with ID {id} not found.");
                }
                return user;
            }

            public async Task AddAsync(User user)
            {
                await _userRepository.AddAsync(user);
            }

            public async Task UpdateAsync(Guid Id, string username, string email, string password)
            {
   
                await _userRepository.UpdateAsync(Id, username,email,password);
            }

            public async Task DeleteAsync(Guid id)
            {
                await _userRepository.DeleteAsync(id);
            }
        }
    }

}
