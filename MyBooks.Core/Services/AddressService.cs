using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MyBooks.Data.Interfaces;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using MyBooks.Core.Profiles;

namespace MyBooks.Core.Services
{
   public class AddressService: IAddressService
    {
        private readonly MyBooksDBContext _context;

        public AddressService(MyBooksDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddressEntity>> GetAllAddressAsync(bool includeMember)
        {
            var addressesModel = await _context.Addresses.ToListAsync();

            if (includeMember)
            {
                addressesModel = await _context.Addresses.Include(e => e.User)
                .ToListAsync();
            }

            var result = Mapping.Mapper.Map<IEnumerable<AddressEntity>>(addressesModel);
            return result;
        }


        public async Task<AddressEntity> GetUserAddressByIdAsync(int userId, int addressId)
        {

            var addressesModel = await _context.Addresses.Include(e => e.User)
                            .FirstOrDefaultAsync(m => m.AddressId == addressId && m.User.UserId == userId);

            var result = Mapping.Mapper.Map<AddressEntity>(addressesModel);

            return result;
        }

        public async Task<AddressEntity> CreateUserAddressAsync(int userId, AddressEntity address)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);
            var newAddress = Mapping.Mapper.Map<Addresses>(address);

            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();

            newAddress.User = userModel;
            await _context.SaveChangesAsync();

            var result = await _context.Addresses.Include(e => e.User)
                .FirstOrDefaultAsync(e => e.AddressId == newAddress.AddressId);

            return Mapping.Mapper.Map<AddressEntity>(result);
        }

        public async Task<AddressEntity> EditUserAddressAsync(int userId, int addressId, AddressEntity address)
        {
            var addressesModel = await _context.Addresses.Include(e => e.User)
                          .FirstOrDefaultAsync(m => m.AddressId == addressId && m.User.UserId == userId);

            var newModel = Mapping.Mapper.Map(address, addressesModel);
            await _context.SaveChangesAsync();

            var result = Mapping.Mapper.Map<AddressEntity>(newModel);
            //result.SupplierId = supplierId;
            return result;

        }

        public async Task DeleteUserAddressAsync(int userId, int addressId)
        {
            var address = await _context.Addresses
                     .FirstOrDefaultAsync(m => m.AddressId == addressId && m.User.UserId == userId);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

    }
}
