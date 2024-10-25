using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Models;

namespace TelegramClone.Core.Abstractions.IRepository
{
    public interface IContactRepository
    {
        //Task<Contact> GetContactByIdAsync(Guid contactId); // Получить контакт по ID
        ///Task<IEnumerable<Contact>> GetContactsByUserIdAsync(Guid userId); // Получить все контакты пользователя по его ID
        Task<Contact> CreateContactAsync(Contact contact); // Создать новый контакт
        //Task UpdateContactAsync(Contact contact); // Обновить контакт
        //Task DeleteContactAsync(Guid contactId); // Удалить контакт
    }

}
