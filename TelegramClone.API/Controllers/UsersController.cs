using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramClone.API.Contracts;
using TelegramClone.Application.Services.TelegramClone.Services;
using TelegramClone.Core.Abstractions;
using TelegramClone.Core.Models;


namespace TelegramClone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequest request)
        {

            var (user, error) = TelegramClone.Core.Models.User.Create(Guid.NewGuid(), request.Username, request.Email, request.PasswordHash);

            if (error != null)
            {
                return BadRequest(error); 
            }

            await _userService.AddAsync(user); 
            return Ok(user); 
        }



        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserRequest request)
        {
            // Найти существующего пользователя
            var existingUser = await _userService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // Создать объект пользователя с обновленными данными
            var (updatedUser, error) = TelegramClone.Core.Models.User.Create(id, request.Username, request.Email, request.PasswordHash);

            if (error != null)
            {
                return BadRequest(error);
            }

            // Обновить пользователя в базе данных
            await _userService.UpdateAsync(updatedUser.Id, updatedUser.Username, updatedUser.Email, updatedUser.PasswordHash);

            return Ok(updatedUser); // Возвращает обновленные данные пользователя
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
