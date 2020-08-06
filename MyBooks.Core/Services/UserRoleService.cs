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
   public class UserRoleService : IUserRolesService
    {
        private readonly MyBooksDBContext _context;
        public UserRoleService(MyBooksDBContext context)
        {
            _context = context;
        }

        public bool CheckIfNewUserRoleExist(UserRolesEntity userRole)
        {
            return _context.UserRoles.
                     FirstOrDefault(e => e.Title == userRole.Title) == null ? true : false;
        }


        //public bool CheckIfUserRoleExist(int userRoleId, UserRolesEntity userRole)
        //{
        //    var result = _context.userRoles.FirstOrDefault(e => e.Title == userRole.Title);
        //    return _context.userRoles.
        //        FirstOrDefault(e => e.Title == userRole.Title) == null || result.Title == userRole.Title ? true : false;
        //}

        public async Task<IEnumerable<UserRolesEntity>> GetAllUserRolesAsync()
        {
            var userRoleModel = await _context.UserRoles
                .ToListAsync();

            var result = Mapping.Mapper.Map<IEnumerable<UserRolesEntity>>(userRoleModel);
            return result;
        }

        public async Task<UserRolesEntity> GetUserRolesByIdAsync(int userRoleId, bool includeUsers)
        {
            var userRoleModel = await _context.UserRoles.Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.UserRoleId == userRoleId);

            if (!includeUsers)
            {
                userRoleModel = await _context.UserRoles
                    .FirstOrDefaultAsync(m => m.UserRoleId == userRoleId);
            }

            var result = Mapping.Mapper.Map<UserRolesEntity>(userRoleModel);

            return result;
        }

        public async Task<UserRolesEntity> CreateUserRolesAsync(UserRolesEntity userRole)
        {

            var result = Mapping.Mapper.Map<UserRoles>(userRole);

            await _context.UserRoles.AddAsync(result);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<UserRolesEntity>(result);
        }

        public async Task<UserRolesEntity> EditUserRolesAsync(int userRoleId, UserRolesEntity userRole)
        {

            var oldmodel = await _context.UserRoles.Include(e => e.Users)
               .FirstOrDefaultAsync(e => e.UserRoleId == userRoleId);

            var newModel = Mapping.Mapper.Map(userRole, oldmodel);
            //await EditSupplierAddressAsync(supplierId, supplier.Address);
            await _context.SaveChangesAsync();

            var result = Mapping.Mapper.Map<UserRolesEntity>(newModel);

            return result;
        }

        public async Task DeleteUserRolesAsync(int userRoleId)
        {

            var userRole = await _context.UserRoles
                       .FirstOrDefaultAsync(e => e.UserRoleId == userRoleId);

            if (userRole.IsActive == true)
            {
                userRole.IsActive = false;
            }
            else
            {
                userRole.IsActive = true;
            }
            await _context.SaveChangesAsync();
        }

    }
}
