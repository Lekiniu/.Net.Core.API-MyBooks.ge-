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
    public class ShopCartService :IShopCartService
    {
        private readonly MyBooksDBContext _context;
        public ShopCartService(MyBooksDBContext context)
        {
            _context = context;
        }


        public bool checkIfShopCartExist(int userId)
        {
            return _context.Shopping_Carts.Where(e=>e.UserId == userId && e.IsOrder == false) != null ? true : false; 
        }
        public bool checkIfShopCartitemExist(int userId, int cartId, int bookId )
        {
            return _context.Cart_Items.FirstOrDefault(m => m.Shop_Carts.UserId == userId && m.CartId == cartId &&m.BookId == bookId) != null ? true : false;
        }


        public async Task<Shopping_Cart_Entity> GetShopCartByIdAsync(int userId, int cartId)
        {
            var shopCartModel = await _context.Shopping_Carts
                 .Include(m => m.User)
                 .Include(m => m.Cart_Items)
                    .ThenInclude(m => m.Book)
                    .FirstOrDefaultAsync(m => m.User.UserId == userId && m.CartId == cartId);

            var result = Mapping.Mapper.Map<Shopping_Cart_Entity>(shopCartModel);

            return result;
        }

        public async Task CreateShopCartAsync(int userId, Shopping_Cart_Entity shopCart)
        {
          
            if (checkIfShopCartExist(userId))
            {
                var userModel = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);
                var newShopCart = Mapping.Mapper.Map<Shopping_Carts>(shopCart);

                await _context.Shopping_Carts.AddAsync(newShopCart);
                await _context.SaveChangesAsync();


                newShopCart.User = userModel;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<Shopping_Cart_Entity> CheckOutShopCartAsync(int userId, int cartId)
        {
            var shopCartModel = await _context.Shopping_Carts.Include(m => m.Cart_Items)
           .FirstOrDefaultAsync(m => m.User.UserId == userId && m.CartId == cartId);

            if (shopCartModel.IsOrder == false)
            {
                shopCartModel.IsOrder = true;
                await CreateShopCartAsync(userId, new Shopping_Cart_Entity
                {
                    Date = DateTime.Now,
                    UserId = userId,
                    IsOrder = false
                });
                await _context.SaveChangesAsync();
            }

            var result = Mapping.Mapper.Map<Shopping_Cart_Entity>(shopCartModel);
            return result;

        }


        public async Task FullDeleteShopCartAsync(int userId, int cartId)
        {

            var shopItems = await _context.Cart_Items
                .Where(e => e.CartId == cartId)
                .ToListAsync();

            var shopCart = await _context.Shopping_Carts
                        .FirstOrDefaultAsync(m => m.User.UserId == userId && m.CartId == cartId);


            _context.Cart_Items.RemoveRange(shopItems);
            _context.Shopping_Carts.Remove(shopCart);

            await _context.SaveChangesAsync();

        }


        public async Task<Cart_Items_Entity> AddCartItemAsync(int userId, int cartId, Cart_Items_Entity cartItem)
        {
            var shopCartModel = await _context.Shopping_Carts.FirstOrDefaultAsync(m => m.User.UserId == userId && m.CartId == cartId && m.IsOrder == false);
            var newCartItem = Mapping.Mapper.Map<Cart_items>(cartItem);

            await _context.Cart_Items.AddAsync(newCartItem);
            await _context.SaveChangesAsync();


            newCartItem.Shop_Carts = shopCartModel;
            await _context.SaveChangesAsync();

            var result = await _context.Cart_Items.Include(e => e.Shop_Carts)
                .FirstOrDefaultAsync(e => e.ItemId == newCartItem.ItemId);

            return Mapping.Mapper.Map<Cart_Items_Entity>(result);
        }

        public async Task<Cart_Items_Entity> EditCartItemAsync(int userId, int cartId, int itemId, Cart_Items_Entity cartItem)
        {
             var shopCartModel = await _context.Shopping_Carts
                    .FirstOrDefaultAsync(m => m.User.UserId == userId && m.CartId == cartId && m.IsOrder == false);

            var cartItemModel = await _context.Cart_Items.Include(e => e.Shop_Carts)
                          .FirstOrDefaultAsync(e => e.ItemId == itemId && e.CartId == shopCartModel.CartId);

            var newModel = Mapping.Mapper.Map(cartItem, cartItemModel);
            await _context.SaveChangesAsync(); 

            var result = Mapping.Mapper.Map<Cart_Items_Entity>(newModel);
            //result.SupplierId = supplierId;
            return result;
        }

        public async Task DeleteCartItemAsync( int[] itemsIds)
        {
            foreach (var itemId in itemsIds)
            {
                var items = await _context.Cart_Items
                         .Where(m => m.ItemId == itemId).ToListAsync();
                _context.Cart_Items.RemoveRange(items); 
            }
            await _context.SaveChangesAsync();
        }
    }
}
