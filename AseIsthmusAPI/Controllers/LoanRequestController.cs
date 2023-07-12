using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using AseIsthmusAPI.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestController : ControllerBase
    {

        private readonly LoanRequestService _service;
        private readonly EmailService _emailService;
        private readonly DocumentsService _documentService;

        public LoanRequestController(LoanRequestService service, EmailService emailService, DocumentsService documentService)
        {
            _service = service;
            _emailService = emailService;
            _documentService = documentService;
            _documentService = documentService;
        }
        #region Get

        [Authorize(Policy = "Loan-Approvers")]
        [HttpGet]
        public async Task<IEnumerable<LoanRequestOutDto>> Get()
        {
            return await _service.GetAll();
        }


        [Authorize(Policy = "Loan-Approvers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanRequestOutDto>> GetById([FromRoute] int id)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
                return loan;
            }
        }
        #endregion

        #region Create

        [Authorize]
        [HttpPost("{id}")]
    public async Task<IActionResult> Create([FromRoute] string id, LoanRequestInDto loan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {
            await _service.Create(id, loan);
            return Ok(loan);
        }
    }
        #endregion

        #region Update

        [Authorize(Policy = "Loan-Approvers")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ApproveLoan([FromRoute] int id, [FromBody] LoanRequestInByAdminDto loan)
        {
            HtmlContentProvider emailTemplate = new HtmlContentProvider();
            var loanToUpdate = await _service.ApproveLoan(id, loan);

            if (loanToUpdate.Item1 is not null)
            {
                if (loan.IsApproved == true)
                {
                    string pagare = "Pagare";
                    string googleDriveLink = await _documentService.FetchGoogleLink(pagare);

                    _emailService.SendEmail(emailTemplate.PagareEmailContent(loanToUpdate.Item2), "Firma de Pagaré", loanToUpdate.Item1, googleDriveLink);
                    return NoContent();
                }
                else {
                    _emailService.SendEmail(emailTemplate.LoanRejectedEmailContent(loanToUpdate.Item2), "Solicitud de Préstamo Rechazada", loanToUpdate.Item1);
                    return NoContent();
                }
            }
            else
            {
                return NotFound(new { error = "No se pudo actualizar el préstamo." });
            }
        }

        [Authorize(Policy = "Administrator")]
        [HttpPatch("request-review/{id}")]
        public async Task<ActionResult> RequestLoanReview([FromRoute] int id)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
              await  _service.RequestLoanReview(id);
                return NoContent();
            }
        }

        [Authorize(Policy = "Loan-Approvers")]
        [HttpPatch("respond-review/{id}")]
        public async Task<ActionResult> RespondLoanReview([FromRoute] int id, [FromBody] bool response)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
               await _service.RespondLoanReview(id, response);
                return NoContent();
            }
        }
        #endregion

        #region methods

        [HttpPost("calculation")]
        public async Task<IActionResult> GetLoanCalculation([FromBody] LoanCalculationType loanCalculation)
        {
            var result = await _service.GetLoanCalculation(loanCalculation);


            return Ok(result);
        }

        #endregion

        #region Delete

        [Authorize(Policy = "Loan-Approvers")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { error = "No se pudo eliminar el préstamo." });
            }
        }
        #endregion
    }
}
