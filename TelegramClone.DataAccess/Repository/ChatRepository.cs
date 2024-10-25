using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       

        // Создать новый чат
        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            var chatEntity = MapToChatEntity(chat);

            _context.Chats.Add(chatEntity);
            await _context.SaveChangesAsync();

            return chat;
        }

      

        // Маппинг Chat -> ChatEntity
        private ChatEntity MapToChatEntity(Chat chat)
        {
            return new ChatEntity
            {
                Id = chat.Id,
                Name = chat.Name,
                UserIds = (List<Guid>)chat.UserIds,
                Messages = chat.Messages.Select(m => new ChatMessageEntity
                {
                    Id = m.Id,
                    ChatId = m.ChatId,
                    SenderUsername = m.SenderUsername,
                    Content = m.Content,
                    Timestamp = m.Timestamp
                }).ToList()
            };
        }
    }

}
