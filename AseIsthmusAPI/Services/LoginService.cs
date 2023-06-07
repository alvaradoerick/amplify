using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AseIsthmusAPI.Services
{
    public class LoginService
    {
        private readonly AseItshmusContext _context;
        private readonly PasswordService _passwordService;
        public LoginService(AseItshmusContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService; 
        }

        public async Task<Login?> GetLogin(LoginDto login)
        {
            var loginEntity = await _context.Logins
       .Include(l => l.Person)
       .FirstOrDefaultAsync(x => x.Person.EmailAddress == login.EmailAddress && x.Pw == _passwordService.HashPassword(login.Pw));

            if (loginEntity == null)
            {
              return null;
            }

            return loginEntity ;
        }

        
    }
}
