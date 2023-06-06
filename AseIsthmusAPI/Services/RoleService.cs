using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AseIsthmusAPI.Services
{
    public class RoleService
    {
        private readonly AseItshmusContext _context;
        public RoleService(AseItshmusContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<string?> GetRoleDescriptionById(int id)
        {
            var roleDescription = await _context.Roles
       .Where(role => role.RoleId == id)
       .Select(role => role.RoleDescription)
       .FirstOrDefaultAsync();

            return roleDescription;
        }
    }
}
