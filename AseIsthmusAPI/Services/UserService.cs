using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace AseIsthmusAPI.Services
{
    public class UserService
    {
        private readonly AseItshmusContext _context;
        public UserService(AseItshmusContext context)
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
                EnrollmentDate = a.EnrollmentDate,
                PhoneNumber = a.PhoneNumber,
                EmailAddress = a.EmailAddress,
                BankAccount = a.BankAccount,
                IsActive = a.IsActive,
                RoleDescription = a.Role.RoleDescription,
                RoleId = a.RoleId,
                Address1 = a.Address1,
                Address2 = a.Address2,
                DistrictId = a.DistrictId,
                PostalCode = a.PostalCode,
                ApprovedDate = a.ApprovedDate
            }).ToListAsync();
        }

        /// <summary>
        /// gets user data by personId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                    RoleDescription = a.Role.RoleDescription,
                    EnrollmentDate = a.EnrollmentDate,
                    RoleId = a.RoleId,
                    Address1 = a.Address1,
                    Address2 = a.Address2,
                    DistrictId = a.DistrictId,
                    PostalCode = a.PostalCode,
                    ApprovedDate = a.ApprovedDate
                }).SingleOrDefaultAsync();
        }

        /// <summary>
        /// finds user by employee code (personId)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        
        /// <summary>
        /// finds user by identification number (number Id)
        /// </summary>
        /// <param name="numberId"></param>
        /// <returns></returns>
        public async Task<User?> GetByNumberId(string numberId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.NumberId == numberId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// finds user by email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.EmailAddress == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> Create(UserDtoIn user)
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
                DistrictId = user.DistrictId,
                PostalCode = user.PostalCode,
                ApprovedDate = user.ApprovedDate
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        /// <summary>
        /// This method updates the profile by the admin; however it does not activate or inactivate the user
        /// </summary>
        /// <param name="userByAdmin"></param>
        /// <returns></returns>
        public async Task UpdateUserByAdmin( UpdateProfileAdminDto userByAdmin)
        {
            var existingClient = await GetById(userByAdmin.PersonId);

            if (existingClient is not null)
            {
                existingClient.PersonId = userByAdmin.PersonId;
                existingClient.NumberId = userByAdmin.NumberId;
                existingClient.FirstName = userByAdmin.FirstName;
                existingClient.LastName1 = userByAdmin.LastName1;
                existingClient.LastName2 = userByAdmin.LastName2;
                existingClient.DateBirth = userByAdmin.DateBirth;
                existingClient.WorkStartDate = userByAdmin.WorkStartDate;
                existingClient.EnrollmentDate = userByAdmin.EnrollmentDate;
                existingClient.RoleId = userByAdmin.RoleId;      
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// allows user to update their own profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateUserByUser(string id, UserUpdateDto user)
        {
            var existingClient = await GetById(id);
            if (existingClient is not null)
            {
                existingClient.PhoneNumber = user.PhoneNumber;
                existingClient.BankAccount = user.BankAccount;
                existingClient.Address1 = user.Address1;
                existingClient.Address2 = user.Address2;
                existingClient.DistrictId = user.DistrictId;
                existingClient.PostalCode = user.PostalCode;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> DeleteUser(string id)
        {
            var existingClient = await GetById(id);
            if (existingClient is not null)
            {
                try
                {
                    _context.Users.Remove(existingClient);
                    await _context.SaveChangesAsync();
                    return string.Empty;
                }
                catch (DbUpdateException ex)
                {
                    var errorMessages = new List<string>();
                    if (ex.InnerException is SqlException sqlException)
                    {
                        foreach (SqlError error in sqlException.Errors)
                        {
                            if (error.Number == 50000) 
                            {
                                errorMessages.Add(error.Message);
                            }
                        }
                    }
                    return string.Join(Environment.NewLine, errorMessages);
                }
            }
            return "User not found.";
        }

        /// <summary>
        /// vaidation if user exists based on different checks
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string?> DuplicateAccount(UserDtoIn user)
        {
            string result = "valid";
            var userExistById = await GetById(user.PersonId);

            var userExistByNumberId = await GetByNumberId(user.NumberId);

            var userExistByEmail = await GetByEmail(user.EmailAddress);

             if (userExistById is not null)
                result = "user exists by Id";

            else if (userExistByNumberId is not null)
                result = "user exists by numberId";

            else if (userExistByEmail is not null)
                result = "user exists by email";

            return result;
        }

        /// <summary>
        /// Updates the status of the user either inactivating it or activating it
        /// </summary>
        /// <returns></returns>
        public async Task<string?> ManageUserStatus(string id) {

           var date = DateTime.Now.Date;
            var existingClient = await GetById(id);
            if (existingClient is not null && existingClient.IsActive == true)
            {
                existingClient.IsActive = false;
                existingClient.ApprovedDate = existingClient.ApprovedDate;
                await _context.SaveChangesAsync();
                return "Deactivated";
            }
            else if (existingClient is not null && existingClient.IsActive == false)
            {
                existingClient.IsActive = true;
                existingClient.ApprovedDate = DateTime.Now.Date;
                await _context.SaveChangesAsync();
                return "Activated";
            }
            else return null;
        }
    }
}
