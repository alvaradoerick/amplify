using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/users/{id}/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly BeneficiaryService _service;
        public BeneficiaryController(BeneficiaryService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> Insert([FromRoute] string id, List<BeneficiaryDtoIn> beneficiaries)
        {

            if (beneficiaries == null || beneficiaries.Count == 0)
            {
                return BadRequest("No beneficiaries provided.");
            }
            try
            {
                foreach (var beneficiary in beneficiaries)
                {
                    var newUser = await _service.Create(id,beneficiary);
                }

                return Ok("Beneficiaries added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
        }
        //    [HttpPost]
        //    [Route("{id}/beneficiary")]
        //    public IActionResult InsertBeneficiary(List<Beneficiary> beneficiaries, string id)
        //    {

        //        using SqlConnection conn = new(_configuration.GetConnectionString("AseIsthmusConn").ToString());
        //        if (conn.State == ConnectionState.Closed)
        //            conn.Open();

        //        if (beneficiaries == null || beneficiaries.Count == 0)
        //        {
        //            return BadRequest("No beneficiaries provided.");
        //        }
        //        try
        //        {
        //            foreach (var beneficiary in beneficiaries)
        //            {
        //                using (var command = new SqlCommand("sp_InsertBeneficiary", conn))
        //                {
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.Parameters.AddWithValue("@beneficiaryName", beneficiary.BeneficiaryName);
        //                    command.Parameters.AddWithValue("@beneficiaryNumberId", beneficiary.BeneficiaryNumberId);
        //                    command.Parameters.AddWithValue("@beneficiaryRelation", beneficiary.BeneficiaryRelation);
        //                    command.Parameters.AddWithValue("@beneficiaryPercentage", beneficiary.BeneficiaryPercentage);
        //                    command.Parameters.AddWithValue("@userId", id);

        //                    command.ExecuteNonQuery();
        //                }
        //            }

        //            conn.Close();

        //            return Ok("Beneficiaries added successfully.");
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }

        //    }
    }
}
