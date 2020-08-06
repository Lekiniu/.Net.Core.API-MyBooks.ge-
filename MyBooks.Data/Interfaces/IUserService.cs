using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;

namespace MyBooks.Data.Interfaces
{
    public interface IUserService
    {
        bool CheckIfNewUserExist(UserEntity user);

        bool CheckIfUserExist(int userId, UserEntity user);

        Task<IEnumerable<UserEntity>> GetAllUsersAsync();

        Task<UserEntity> GetUserByIdAsync(int userId, bool includeBooks);

        Task<UserEntity> CreateUserAsync(UserEntity user);

        Task<UserEntity> EditUserAsync(int userId, UserEntity user);

        //Task EditUserAddressAsync(int userId, AddressEntity address);

        Task FullDeleteUserAsync(int userId);

        Task DeleteUserAsync(int userId);
    }
}
