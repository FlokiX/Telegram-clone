using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(Guid Id, string username, string email, string password);
        Task DeleteAsync(Guid id);
    }
}
