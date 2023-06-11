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
            string validationResult = await _service.AccountExist(user);

            if (validationResult.Equals("user exists by Id"))
                return BadRequest(new { error = "El usuario con el código de empleado ingresado ya existe en el sistema. Contacte al administrador." });
            else if (validationResult.Equals("user exists by numberId"))
                return BadRequest(new { error = "El usuario con la identificación ingresada ya existe en el sistema. Contacte al administrador." });
            else if (validationResult.Equals("user exists by email"))
                return BadRequest(new { error = "El usuario con el correo ingresado ya existe en el sistema. Contacte al administrador." });
            else if (!ModelState.IsValid)
                return BadRequest(new { error = "Faltan datos por ingresar." });
            else {
                var newUser = await _service.Create(user);

                return CreatedAtAction(nameof(GetById), new { id = newUser.PersonId }, newUser);
            }

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, UserDtoIn user)
        {

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
            return NotFound(new { error = $"El usuario con código={id} no existe." });
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserByUser([FromRoute] string id, UserUpdateDto user)
        {
            var existingClient = await _service.GetById(id);
            try
            {
                if (existingClient is not null)
                {
                    await _service.UpdateUserByUser(id, user);
                     return Ok(new { message = "successful" });
                }
                else
                {
                    return UserNotFound(id);
                }
            }
            catch (ArgumentException)
            {
                return BadRequest(new { error = "No se pudo procesar su pedido." });
            }
        }

    }
}
