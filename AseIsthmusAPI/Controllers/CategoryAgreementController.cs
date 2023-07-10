using Microsoft.AspNetCore.Mvc;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data;

namespace AseIsthmusAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAgreementController : ControllerBase
    {
        private readonly CategoryAgreementService _service;


        public CategoryAgreementController(CategoryAgreementService service)
        {
            _service = service;
        }

        #region Get

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<CategoryAgreement>> Get()
        {
            return await _service.GetAll();
        }

        [Authorize]
        [HttpGet("active-categories")]
        public async Task<IEnumerable<CategoryAgreement>> GetAllActiveCategories()
        {
            return await _service.GetAllActiveCategories();
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryAgreement>> GetById([FromRoute] int id)
        {
            var categoryAgreement = await _service.GetById(id);

            if (categoryAgreement is null)
            {
                return NotFound(new { error = "No se pudo encontrar ninguna categoría." });
            }
            else
            {
                return categoryAgreement;
            }
        }

        #endregion

        #region Create

        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryAgreement categoryAgreement)
        {
            var newCategoryAgreement = await _service.Create(categoryAgreement);

            return CreatedAtAction(nameof(GetById), new { id = newCategoryAgreement.CategoryAgreementId }, newCategoryAgreement);
        }
        #endregion

        #region Update

        [Authorize(Policy = "Administrator")]
        [HttpPut("{id}")]       
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryAgreement categoryAgreement)
        {
            var categoryAgreementToUpdate = await _service.GetById(id);

            if (categoryAgreementToUpdate is not null)
            {
                await _service.Update(id, categoryAgreement);
                return NoContent();
            }
            else
            {
                return NotFound(new { error = "No se pudo actualizar la categoría." });
            }
        }
        #endregion

        #region Delete
        [Authorize(Policy = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            bool hasAgreements = await _service.HasAgreements(id);

            if (hasAgreements)
            {
                return BadRequest(new { error = "No se puede eliminar la categoría porque tiene convenios asociados." });
            }
            else
            {
                var categoryAgreementToDelete = await _service.GetById(id);

                if (categoryAgreementToDelete is not null)
                {
                    await _service.Delete(id);
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
        }
        #endregion
    }
}
