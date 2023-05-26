using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AseIsthmusAPI.Services
{
    public class UserService
    {
        private readonly AseIsthmusContext _context;
        public UserService(AseIsthmusContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDtoOut>> GetAll()
        {
            return await _context.Users.Select(a => new UserDtoOut 
            {
                 PersonId = a.PersonId,
                NumberId = a.NumberId,
                FirstName = a.FirstName,
                LastName1 = a.LastName1,
                LastName2 = a.LastName2,
                Nationality = a.Nationality,
                DateBirth = a.DateBirth,
                WorkStartDate = a.WorkStartDate,
                PhoneNumber = a.PhoneNumber,
                EmailAddress = a.EmailAddress,
                BankAccount = a.BankAccount,
                IsActive = a.IsActive,
                RoleId = a.Role.RoleDescription,
                Address1 = a.Address1,
                Address2 = a.Address2,
                Province = a.Province,
                Canton = a.Canton,
                District = a.District,
                PostalCode = a.PostalCode,
                ApprovedDate = a.ApprovedDate
            }).ToListAsync();
        }

        public async Task<UserDtoOut?> GetDtoById(string id)
        {
            return await _context.Users.Where(a => a.PersonId == id).
                Select(a => new UserDtoOut
            {
                PersonId = a.PersonId,
                NumberId = a.NumberId,
                FirstName = a.FirstName,
                LastName1 = a.LastName1,
                LastName2 = a.LastName2,
                Nationality = a.Nationality,
                DateBirth = a.DateBirth,
                WorkStartDate = a.WorkStartDate,
                PhoneNumber = a.PhoneNumber,
                EmailAddress = a.EmailAddress,
                BankAccount = a.BankAccount,
                IsActive = a.IsActive,
                RoleId = a.Role.RoleDescription,
                Address1 = a.Address1,
                Address2 = a.Address2,
                Province = a.Province,
                Canton = a.Canton,
                District = a.District,
                PostalCode = a.PostalCode,
                ApprovedDate = a.ApprovedDate
            }).SingleOrDefaultAsync();
        }

        public async Task<User?> GetById(string Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<User> Create(UserDtoIn user)
        {
            var newUser = new User
            {
                PersonId = user.PersonId,
                NumberId = user.NumberId,
                FirstName = user.FirstName,
                LastName1 = user.LastName1,
                LastName2 = user.LastName2,
                Nationality = user.Nationality,
                DateBirth = user.DateBirth,
                WorkStartDate = user.WorkStartDate,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                BankAccount = user.BankAccount,
                IsActive = false,
                RoleId = 4,
                Address1 = user.Address1,
                Address2 = user.Address2,
                Province = user.Province,
                Canton = user.Canton,
                District = user.District,
                PostalCode = user.PostalCode,
                ApprovedDate = user.ApprovedDate
            };
            _context.Users.Add(newUser);
           await _context.SaveChangesAsync();

            return newUser;
        }
        public async Task UpdateUser(UserDtoIn user)
        {
            var existingClient = await GetById(user.PersonId);

            if (existingClient is not null)
            {

                existingClient.PersonId = user.PersonId;
                existingClient.NumberId = user.NumberId;
                existingClient.FirstName = user.FirstName;
                existingClient.LastName1 = user.LastName1;
                existingClient.LastName2 = user.LastName2;
                existingClient.Nationality = user.Nationality;
                existingClient.DateBirth = user.DateBirth;
                existingClient.WorkStartDate = user.WorkStartDate;
                existingClient.PhoneNumber = user.PhoneNumber;
                existingClient.EmailAddress = user.EmailAddress;
                existingClient.BankAccount = user.BankAccount;
                existingClient.IsActive = existingClient.IsActive;
                existingClient.RoleId = user.RoleId;
                existingClient.Address1 = user.Address1;
                existingClient.Address2 = user.Address2;
                existingClient.Province = user.Province;
                existingClient.Canton = user.Canton;
                existingClient.District = user.District;
                existingClient.PostalCode = user.PostalCode;
                existingClient.ApprovedDate = existingClient.ApprovedDate;

               await _context.SaveChangesAsync();
            }
        }
    
        public async Task DeleteUser(string id)
        {
            var existingClient = await GetById(id);
            if (existingClient is not null)
            {
                _context.Users.Remove(existingClient);
               await _context.SaveChangesAsync();

            }
        }

    }
}
