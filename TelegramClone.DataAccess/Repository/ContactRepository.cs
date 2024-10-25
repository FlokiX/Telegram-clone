using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Entites;

namespace TelegramClone.DataAccess.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            // Преобразование из доменной модели в сущность
            var entity = new ContactEntity
            {
                Id = contact.Id, // Или Guid.NewGuid() для генерации нового ID
                UserId = contact.UserId,
                ContactUsername = contact.ContactUsername,
                ChatId = contact.ChatId
            };

            // Добавить новую сущность в контекст и сохранить изменения
            _context.Set<ContactEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return contact;
        }
    }

}
