using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _service.GetById(id);
            if (user is null) return UserNotFound(id);

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserDTO user)
        {
            string validationResult = await ValidateAccount(user);

            if (!validationResult.Equals("Valid"))
                return BadRequest(new { message = validationResult });

            var newUser = await _service.Create(user);

            return CreatedAtAction(nameof(GetById), new { id = newUser.PersonId }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UserDTO user)
        {
            string validationResult = await ValidateAccount(user);

            if (!validationResult.Equals("Valid"))
                return BadRequest(new { message = validationResult });

            if (id != user.PersonId)
                return BadRequest(new { message = $"El código({id}) de la URL no coincide con el código({user.PersonId}) de los datos" });

            var existingClient = await _service.GetById(id);

            if (existingClient is not null)
            {
                await _service.UpdateUser(user);
                return NoContent();
            }
            else
            {
                return UserNotFound(id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingClient = await _service.GetById(id);

            if (existingClient is not null)
            {
                await _service.DeleteUser(id);
                return NoContent();
            }
            else
            {
                return UserNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult UserNotFound(string id)
        {
            return NotFound(new { message = $"El usuario con código={id} no existe." });
        }

        [NonAction]
        public async Task<string> ValidateAccount(UserDTO user)
        {
            string result = "Valid";
            var userExist = await _service.GetById(user.PersonId);
            if (userExist is null)
                result = $"El usuario con código {user.PersonId} no existe.";

            return result;
        }

    }
}
