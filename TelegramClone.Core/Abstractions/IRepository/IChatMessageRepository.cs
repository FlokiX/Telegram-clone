using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IRepository
{
    public interface IChatMessageRepository
    {
        //Task<ChatMessage> GetMessageByIdAsync(Guid messageId); // Получить сообщение по ID
        //Task<IEnumerable<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId); // Получить все сообщения чата по ID чата
        Task<ChatMessage> CreateMessageAsync(ChatMessage message); // Создать новое сообщение
        //Task UpdateMessageAsync(ChatMessage message); // Обновить сообщение
        //Task DeleteMessageAsync(Guid messageId); // Удалить сообщение
    }

}
