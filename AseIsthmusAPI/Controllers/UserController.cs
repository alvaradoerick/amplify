using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Insert(UserDTO user)
        {
            var newUser = await _service.Create(user);

            return CreatedAtAction(nameof(GetById), new { id = newUser.PersonId }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, UserDTO user)
        {
            if (id != user.PersonId)
                return BadRequest(new { message = $"El  ID={id} de la URL no coincide con el ID({user.PersonId}) de la solicitud" });

            var existingClient = await _service.GetById(id);

            if (existingClient is not null)
            {
                await _service.UpdateUser(id, existingClient);
                return NoContent();
            }
            else
            {
                return UserNotFound(id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
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

        public NotFoundObjectResult UserNotFound(string id)
        {
            return NotFound(new { message = $"El usuario con ID={id} no existe." });
        }

    }
}
