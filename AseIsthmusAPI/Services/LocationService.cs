using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace AseIsthmusAPI.Services
{
    public class LocationService
    {
        private readonly AseItshmusContext _context;


        public LocationService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationDtoOut>> GetCantonsByProvince(int provinceId)
        {
            return await _context.Cantons.Where(c => c.ProvinceId == provinceId).Select(a => new LocationDtoOut
            {
                ProvinceId = a.ProvinceId,
                ProvinceName = a.Province.ProvinceName,
                CantonId = a.CantonId,
                CantonName = a.CantonName
            }).ToListAsync();
        }

        public async Task<IEnumerable<LocationDtoOut>> GetDistrictsByCanton(int cantonId)
        {
            return await _context.Districts.Where(d => d.CantonId == cantonId)
                .Select(a => new LocationDtoOut
            {
                DistrictId = a.DistrictId,
                    DistrictName = a.DistrictName,
                    CantonId = a.CantonId,
                CantonName = a.Canton.CantonName,
                    ProvinceId = a.Canton.ProvinceId,
                    ProvinceName = a.Canton.Province.ProvinceName
                }).ToListAsync();             
        }
    }
}
