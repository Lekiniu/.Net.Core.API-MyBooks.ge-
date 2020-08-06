using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;

namespace MyBooks.Data.Interfaces
{
    public interface IUserRolesService
    {
        bool CheckIfNewUserRoleExist(UserRolesEntity userRole);

        //bool CheckIfUserRoleExist( UserRolesEntity userRole);

        Task<IEnumerable<UserRolesEntity>> GetAllUserRolesAsync();

        Task<UserRolesEntity> GetUserRolesByIdAsync(int userRoleId, bool includeUsers);

        Task<UserRolesEntity> CreateUserRolesAsync(UserRolesEntity userRole);

        Task<UserRolesEntity> EditUserRolesAsync(int userRoleId, UserRolesEntity userRole);

        Task DeleteUserRolesAsync(int userRoleId);
    }
}
