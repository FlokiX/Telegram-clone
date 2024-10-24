using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IRepository
{
    public interface IChatRepository
    {
        Task<Chat> GetChatByIdAsync(Guid chatId); // Получить чат по ID
        Task<IEnumerable<Chat>> GetAllChatsAsync(); // Получить все чаты
        Task<Chat> CreateChatAsync(Chat chat); // Создать новый чат
        Task UpdateChatAsync(Chat chat); // Обновить чат
        Task DeleteChatAsync(Guid chatId); // Удалить чат
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId); // Получить все чаты по ID пользователя
    }

}
