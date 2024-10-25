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
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ChatMessage> CreateMessageAsync(ChatMessage message)
        {
        
            var messageEntity = new ChatMessageEntity
            {
                Id = message.Id,
                ChatId = message.ChatId,
                SenderUsername = message.SenderUsername,
                Content = message.Content,
                Timestamp = message.Timestamp
            };

         
            _context.ChatMessages.Add(messageEntity);
            await _context.SaveChangesAsync();
            return message;
        }
    }
}
