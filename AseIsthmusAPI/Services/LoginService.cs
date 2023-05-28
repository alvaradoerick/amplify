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

        public async Task<Login?> GetLogin(LoginDto login) {

            var user = await _context.Users
        .FirstOrDefaultAsync(u => u.EmailAddress == login.EmailAddress);

            if (user != null)
            {
                return await _context.Logins.SingleOrDefaultAsync(x => x.Pw == login.Pw && user.EmailAddress == login.EmailAddress);
            }
            else {
                return null;
            }

        }
    }
}
