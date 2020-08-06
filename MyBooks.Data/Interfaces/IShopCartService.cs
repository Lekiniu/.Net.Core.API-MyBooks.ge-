using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;

namespace MyBooks.Data.Interfaces
{
    public interface IShopCartService
    {
        bool checkIfShopCartExist(int userId);

        bool checkIfShopCartitemExist(int userId, int cartId, int bookId);

        Task<Shopping_Cart_Entity> GetShopCartByIdAsync(int userId, int cartId);

        Task CreateShopCartAsync(int userId, Shopping_Cart_Entity shopCart);

        Task<Shopping_Cart_Entity> CheckOutShopCartAsync(int userId, int cartId);

        Task FullDeleteShopCartAsync(int userId, int cartId);

        Task<Cart_Items_Entity> AddCartItemAsync(int userId, int cartId, Cart_Items_Entity cartItem);

        Task<Cart_Items_Entity> EditCartItemAsync(int userId, int cartId, int itemId, Cart_Items_Entity cartItem);

        Task DeleteCartItemAsync(int[] itemsIds);
    }
}
