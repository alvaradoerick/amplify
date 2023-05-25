using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;



namespace AseIsthmusAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AseIsthmusContext _context;
        public UserController(AseIsthmusContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(string Id)
        {
            var user = _context.Users.Find(Id);
            if (user is null) return NotFound();

            return user;
        }


        [HttpPost]
        public IActionResult InsertUser(UserDTO user)
        {
            var newUser = new User();

            newUser.PersonId = user.PersonId;
            newUser.NumberId = user.NumberId;
            newUser.FirstName = user.FirstName;
            newUser.LastName1 = user.LastName1;
            newUser.LastName2 = user.LastName2;
            newUser.Nationality = user.Nationality;
            newUser.DateBirth = user.DateBirth; 
            newUser.WorkStartDate = user.WorkStartDate;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.EmailAddress = user.EmailAddress;
            newUser.BankAccount = user.BankAccount;
            newUser.IsActive = false;
            newUser.RoleId = 4;          
            newUser.Address1 = user.Address1; 
            newUser.Address2 = user.Address2;
            newUser.Province = user.Province;
            newUser.Canton = user.Canton;
            newUser.District = user.District;
            newUser.PostalCode = user.PostalCode;
         
             _context.Users.Add(newUser);
                _context.SaveChanges();
            
            return CreatedAtAction(nameof(GetById), new { id = user.PersonId }, user);
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

        //    [HttpGet]
        //    [Route("login")]
        //    public ActionResult Login(Login login)
        //    {

        //        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("AseIsthmusConn").ToString()))
        //        {
        //            try
        //            {


        //                if (conn.State == ConnectionState.Closed)
        //                    conn.Open();
        //                using SqlCommand cmd = new SqlCommand("sp_Login", conn);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.Add(new SqlParameter("@email", login.EmailAddress));
        //                cmd.Parameters.Add(new SqlParameter("@password", login.Pw));
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        reader.Read();
        //                        conn.Close();
        //                        return StatusCode(StatusCodes.Status200OK);
        //                    }
        //                    else
        //                    {
        //                        conn.Close();
        //                        return StatusCode(StatusCodes.Status404NotFound);
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //            }
        //        }

        //    }
    }
}
