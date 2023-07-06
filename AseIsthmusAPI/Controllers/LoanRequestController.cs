﻿using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestController : ControllerBase
    {

        private readonly LoanRequestService _service;

        public LoanRequestController(LoanRequestService service)
        {
            _service = service;
        }
        #region Get

        [HttpPost("calculation")]
        public async Task<IActionResult> GetLoanCalculation([FromBody]LoanCalculationType loanCalculation)
        {
            var result = await _service.GetLoanCalculation(loanCalculation);


            return Ok(result);
        }
        #endregion

        #region Create

        // [Authorize]
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
}
}
