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

        // Создать новый чат или вернуть существующий
        public async Task<Chat> CreateChatAsync(Guid user1Id, Guid user2Id)
        {
            // Проверяем, существует ли уже чат между двумя пользователями
            var existingChat = await _context.Chats
                .FirstOrDefaultAsync(c => (c.User1Id == user1Id && c.User2Id == user2Id) ||
                                           (c.User1Id == user2Id && c.User2Id == user1Id));

            // Если чат уже существует, возвращаем его
            if (existingChat != null)
            {
                return Chat.Create(existingChat.User1Id, existingChat.User2Id).chat; // Возвращаем существующий чат
            }

            // Если чата нет, создаем новый
            var (newChat, error) = Chat.Create(user1Id, user2Id); // Используем метод создания чата
            if (error != null)
            {
                throw new InvalidOperationException(error); // Обработка ошибки, если нужно
            }
            var chatEntity = MapToChatEntity(newChat);

            _context.Chats.Add(chatEntity);
            await _context.SaveChangesAsync();

            return newChat; // Возвращаем созданный чат
        }

        // Маппинг ChatEntity -> Chat
        private Chat MapToChat(ChatEntity chatEntity)
        {
            return Chat.Create(chatEntity.Id, chatEntity.User1Id, chatEntity.User2Id,
                chatEntity.Messages.Select(m => new ChatMessage
                {
                    Id = m.Id,
                    ChatId = m.ChatId,
                    SenderUsername = m.SenderUsername,
                    Content = m.Content,
                    Timestamp = m.Timestamp
                }).ToList());
        }


        // Маппинг Chat -> ChatEntity
        private ChatEntity MapToChatEntity(Chat chat)
        {
            return new ChatEntity
            {
                Id = chat.Id,
                User1Id = chat.User1Id,
                User2Id = chat.User2Id,
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
