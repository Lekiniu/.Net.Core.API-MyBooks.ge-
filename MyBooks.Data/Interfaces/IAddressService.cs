using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;


namespace MyBooks.Data.Interfaces
{
   public  interface IAddressService
    {
        Task<IEnumerable<AddressEntity>> GetAllAddressAsync(bool includeBooks);

        Task<AddressEntity> GetUserAddressByIdAsync(int userId, int addressId);

        Task<AddressEntity> CreateUserAddressAsync(int userId, AddressEntity address);

        Task<AddressEntity> EditUserAddressAsync(int userId, int addressId, AddressEntity address);

        Task DeleteUserAddressAsync(int userId, int addressId);
    }
}
