using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AseIsthmusAPI.Services
{
    public class LoginService
    {
        private readonly AseItshmusContext _context;
        public LoginService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<Login?> GetLogin(LoginDto login)
        {
            var loginEntity = await _context.Logins
           .Include(l => l.Person)
           .FirstOrDefaultAsync(x => x.Pw == login.Pw && x.Person.EmailAddress == login.EmailAddress);
               
            return loginEntity;           
        }
    }
}
