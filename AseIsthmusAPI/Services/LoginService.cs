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
        public LoginService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<Login?> GetLogin(LoginDto login)
        {
            var loginEntity = await _context.Logins
       .Include(l => l.Person)
       .FirstOrDefaultAsync(x => x.Person.EmailAddress == login.EmailAddress && x.Pw == HashPassword(login.Pw));

            if (loginEntity == null)
            {
              return null;
            }

            return loginEntity ;
        }

        /// <summary>
        /// This method is executed when the admin approves the user for the first time
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        public async Task<string?> UpdatePasswordByEmail(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == updatePasswordRequestDto.EmailAddress);
            if (user == null) 
            { 
                return null; 
            } 

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.PersonId == user.PersonId);
            if (login == null)
            {
                return null;
            }

            var newPassword = GenerateRandomPassword();

            login.Pw = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return newPassword;
        }

        /// <summary>
        /// This method is executed when the user is already approved and wants to reset the password
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordByEmail(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == updatePasswordRequestDto.EmailAddress);
                if (user == null)
            {
                return null;
            }

            var login = await _context.Logins.FirstOrDefaultAsync(l => l.PersonId == user.PersonId);
            if (login == null)
            {
                return null;
            }

            else if (user.IsActive == false)
            {
                return "1";
            }

            var newPassword = GenerateRandomPassword();

            login.Pw = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return newPassword;
        }
        public static string GenerateRandomPassword(int length = 8)
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string specialChars = "!@#$%^&*()_-+=<>?/";
            const string numericChars = "0123456789";

            string allChars = uppercaseChars + lowercaseChars + specialChars + numericChars;

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                var chars = randomBytes
                    .Select(b => allChars[b % allChars.Length])
                    .ToArray();

                return new string(chars);
            }
        }
        public string? HashPassword(string password)
        {
            if  (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password)) {
                return null;
            }
            byte[] salt = new byte[128 / 8];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
    }
}
