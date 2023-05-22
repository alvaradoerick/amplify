using AseIsthmusAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("user")]
        public IActionResult InsertPerson(Person person)
        {
            try
            {
                using SqlConnection conn = new(_configuration.GetConnectionString("AseIsthmusConn").ToString());
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("sp_InsertUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@personId", person.PersonId);
                cmd.Parameters.AddWithValue("@numberId", person.NumberId);
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName1", person.LastName1);
                cmd.Parameters.AddWithValue("@lastName2", person.LastName2);
                cmd.Parameters.AddWithValue("@nationality", person.Nationality);
                cmd.Parameters.AddWithValue("@dob", person.DateBirth.ToShortDateString());
                cmd.Parameters.AddWithValue("@workStartDate", person.WorkStartDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);
                cmd.Parameters.AddWithValue("@emailAddress", person.EmailAddress);
                cmd.Parameters.AddWithValue("@bankAccount", person.BankAccount);
                cmd.Parameters.AddWithValue("@address1", person.Address1);
                cmd.Parameters.AddWithValue("@address2", person.Address2);
                cmd.Parameters.AddWithValue("@province", person.Province);
                cmd.Parameters.AddWithValue("@canton", person.Canton);
                cmd.Parameters.AddWithValue("@district", person.District);
                cmd.Parameters.AddWithValue("@postalCode", person.PostalCode);
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    conn.Close();
                    return Ok(person.PersonId);
                    //return Ok("Data inserted successfully");
                }
                else
                {
                    conn.Close();
                    return BadRequest("Failed to insert data");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("{id}/beneficiary")]
        public IActionResult InsertBeneficiary(List<Beneficiary> beneficiaries, string id)
        {

            using SqlConnection conn = new(_configuration.GetConnectionString("AseIsthmusConn").ToString());
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            if (beneficiaries == null || beneficiaries.Count == 0)
            {
                return BadRequest("No beneficiaries provided.");
            }
            try
            {
                foreach (var beneficiary in beneficiaries)
                {
                    using (var command = new SqlCommand("sp_InsertBeneficiary", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@beneficiaryName", beneficiary.BeneficiaryName);
                        command.Parameters.AddWithValue("@beneficiaryNumberId", beneficiary.BeneficiaryNumberId);
                        command.Parameters.AddWithValue("@beneficiaryRelation", beneficiary.BeneficiaryRelation);
                        command.Parameters.AddWithValue("@beneficiaryPercentage", beneficiary.BeneficiaryPercentage);
                        command.Parameters.AddWithValue("@personId", id);

                        command.ExecuteNonQuery();
                    }
                }

                conn.Close();

                return Ok("Beneficiaries added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login(Login login)
        {

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("AseIsthmusConn").ToString()))
            {
                try
                {


                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using SqlCommand cmd = new SqlCommand("sp_Login", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@email", login.EmailAddress));
                    cmd.Parameters.Add(new SqlParameter("@password", login.Pw));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            conn.Close();
                            return StatusCode(StatusCodes.Status200OK);
                        }
                        else
                        {
                            conn.Close();
                            return StatusCode(StatusCodes.Status404NotFound);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }
    }
}
