using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TelegramClone.Core.Abstractions;
using TelegramClone.Core.Abstractions.IRepository;
using TelegramClone.Core.Models;
using TelegramClone.DataAccess.Repository;

namespace TelegramClone.Application.Services
{

    namespace TelegramClone.Services
    {

        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;
            private readonly IContactRepository _contactRepository;
            public UserService(IUserRepository userRepository, IContactRepository contactRepository)
            {
                _userRepository = userRepository;
                _contactRepository = contactRepository; 
            }

            /*public async Task<IEnumerable<User>> GetAllAsync()
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
            }*/

            public async Task<(User user, string error)> RegisterAsync(string username, string email, string password)
            {
                var (newUser, error) = User.Create(Guid.NewGuid(), username, email, password);

                // Проверка на наличие ошибки при создании пользователя
                if (error != null)
                {
                    return (null, error);
                }

                await _userRepository.AddAsync(newUser);


                var (newContact, contactError) = Contact.Create(newUser.Id, newUser.Username);

              
                if (contactError != null)
                {
                    return (null, contactError); // Возвращаем ошибку, если создание контакта не удалось
                }

                // Сохранение контакта в базе данных
                await _contactRepository.CreateContactAsync(newContact);

                return (newUser, null);

            }



            public async Task<(string token, string error)> LoginAsync(string email, string password)
            {
                // Получаем пользователя по электронной почте
                var (user, error) = await _userRepository.GetByEmailAsync(email);
                if (error != null)
                {
                    return (null, error);
                }

                if (!user.VerifyPassword(password))
                {
                    return (null, "Invalid email or password.");
                }
                var token = GenerateJwtToken(user);

                return (token, null); 
            }


            public string GenerateJwtToken(User user)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GES1cnscrvu5LoiDnnIPON+Lc4U8ODRpFGrzzUnFlIU="));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: null, 
                    audience: null, 
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            /* public async Task UpdateAsync(Guid Id, string username, string email, string password)
             {

                 await _userRepository.UpdateAsync(Id, username,email,password);
             }

             public async Task DeleteAsync(Guid id)
             {
                 await _userRepository.DeleteAsync(id);
             }*/
        }
    }

}
