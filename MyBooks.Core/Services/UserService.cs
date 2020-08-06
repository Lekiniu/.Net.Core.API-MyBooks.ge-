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
   public class UserService : IUserService
    {
        private readonly MyBooksDBContext _context;
        private readonly IShopCartService _shopCartService;
        public UserService(MyBooksDBContext context, IShopCartService shopCartService)
        {
            _context = context;
            _shopCartService = shopCartService;
        }


        public bool CheckIfNewUserExist(UserEntity user)
        {
            return _context.Users.FirstOrDefault(e => e.Email == user.Email) == null ? true : false;
        }


        public bool CheckIfUserExist(int userId, UserEntity user)
        {
            var result =  _context.Users.FirstOrDefault(e => e.Email == user.Email);
            return _context.Users.
                FirstOrDefault(e => e.Email == user.Email) == null || result.Email == user.Email ? true : false;
        }


        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            var userModel = await _context.Users
                .Include(m => m.Address)
                 .Include(m => m.Books)
                .ToListAsync();

            var result = Mapping.Mapper.Map<IEnumerable<UserEntity>>(userModel);
            return result;
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId, bool includeBooks)
        {
            var userModel = await _context.Users.Include(m => m.Address)
                .FirstOrDefaultAsync(m => m.UserId == userId);

            if (includeBooks)
            {
                userModel = await _context.Users
                    .Include(m => m.Books)
                    .FirstOrDefaultAsync(m => m.UserId == userId);
            }

            var result = Mapping.Mapper.Map<UserEntity>(userModel);

            return result;
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {

            var result = Mapping.Mapper.Map<Users>(user);

            if (result.Address != null)
            {
                await _context.Addresses.AddAsync(result.Address);
                await _context.SaveChangesAsync();

                var addressId = result.Address.AddressId;
                result.AddressId = addressId;
            }

            await _context.Users.AddAsync(result);
            await _context.SaveChangesAsync();

            await _shopCartService.CreateShopCartAsync(result.UserId, new Shopping_Cart_Entity {
                Date = DateTime.Now,
                UserId=result.UserId,
                IsOrder=false
            });
            return Mapping.Mapper.Map<UserEntity>(result);
        }

        public async Task<UserEntity> EditUserAsync(int userId, UserEntity user)
        {

            var oldmodel = await _context.Users.Include(e => e.Address)
               .FirstOrDefaultAsync(e => e.UserId == userId);

            var newModel = Mapping.Mapper.Map(user, oldmodel);
            //await EditSupplierAddressAsync(supplierId, supplier.Address);
            await _context.SaveChangesAsync();

            var result = Mapping.Mapper.Map<UserEntity>(newModel);


            return result;
        }

        private async Task EditUserAddressAsync(int userId, AddressEntity address)
        {
            var oldmodel = await _context.Addresses.FirstOrDefaultAsync(e => e.UsesId == userId);
            var result = Mapping.Mapper.Map(address, oldmodel);
            //result.SupplierId = suppliedId;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users
                       .FirstOrDefaultAsync(e => e.UserId == userId);

            if(user.IsActive == true)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            await _context.SaveChangesAsync();
        }


        public async Task FullDeleteUserAsync(int userId)
        {

            var userAddress = await _context.Addresses
                .Where(e => e.UsesId == userId)
                .ToListAsync();

            var books = await _context.Books
                          .Where(e => e.UserId == userId).ToListAsync();

            var booksCatalogs = await _context.Catalogs_Books.Where(f => books.Select(e => e.BookId)
            .Contains(f.BookId))
            .ToListAsync();

            var user = await _context.Users
                        .FirstOrDefaultAsync(e => e.UserId == userId);

             _context.Catalogs_Books.RemoveRange(booksCatalogs);
            _context.Books.RemoveRange(books);
            _context.Addresses.RemoveRange(userAddress);
            _context.Users.Remove(user);
            //var result = Mapping.Mapper.Map(catalog, oldmodel);
            await _context.SaveChangesAsync();

            //return Mapping.Mapper.Map<CatalogsEntity>(result);
        }
    }
}
