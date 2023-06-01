using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserDtoOut>> Get()
        {
           return await _service.GetAll();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDtoOut>> GetById([FromRoute] string id)
        {
            var user = await _service.GetDtoById(id);
            if (user is null) return UserNotFound(id);

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserDtoIn user)
        {
            string validationResult = await ValidateAccount(user);

            if (validationResult.Equals("Valid"))
                return BadRequest(new { message = validationResult });

            var newUser = await _service.Create(user);

            return CreatedAtAction(nameof(GetById), new { id = newUser.PersonId }, newUser);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, UserDtoIn user)
        {
            string validationResult = await ValidateAccount(user);

            if (!validationResult.Equals("Usuario existe en la BD"))
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

        [Authorize]
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
        public async Task<string> ValidateAccount(UserDtoIn user)
        {
            string result = "Usuario existe en la BD";
            var userExist = await _service.GetById(user.PersonId);
            if (userExist is null)
                result = $"El usuario con código {user.PersonId} no existe.";

            return result;
        }

    }
}
