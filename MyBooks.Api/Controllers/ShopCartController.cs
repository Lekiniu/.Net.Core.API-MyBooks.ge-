using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyBooks.Data.Entities;
using MyBooks.Data.Interfaces;
using MyBooks.Api.Filters;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MyBooks.Api.Controllers
{
    [Route("api/users/{userId}")]
    [ApiController]
    public class ShopCartController : Controller
    {

        private readonly IShopCartService _shopCartService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;

        public ShopCartController(IShopCartService shopCartService, LinkGenerator linkGanarator, ILoggerService logger, IUserService  userService)
        {
            _linkGanarator = linkGanarator;
            _logger = logger;
            _shopCartService = shopCartService;
        }

        [HttpGet("CartDetails/{cartId}")]
        public async Task<IActionResult> GetShopCartByIdAsync(int userId, int cartId)
        {

            var model = await _shopCartService.GetShopCartByIdAsync(userId, cartId);
            if (model == null) return NotFound();
            return Ok(model);
        }


        //[HttpPost]
        //[Route("CreateShopCart")]
        //[ModelStateFilter]
        //public async Task<IActionResult> CreateShopCartAsync(int userId, Shopping_Cart_Entity shopCart)
        //{
        //    if (!_shopCartService.checkIfShopCartExist(userId))
        //    {
        //        var model = await _shopCartService.CreateShopCartAsync(userId, shopCart);
        //        var location = _linkGanarator.GetPathByAction("GetShopCartByIdAsync", "ShopCart", new { userId, model.CartId });

        //        return Created(location, model);
        //    }
        //    else
        //    {
        //        return BadRequest("User's shopping cart already exists");
        //    }

        //}


        [HttpPut]
        [Route("CheckOutShopCart/{cartId}")]
        public async Task<ActionResult<Shopping_Cart_Entity>> CheckOutShopCartAsync(int userId, int cartId)
        {

            var model = await _shopCartService.CheckOutShopCartAsync(userId, cartId);
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            var location = _linkGanarator.GetPathByAction("GetShopCartByIdAsync", "ShopCart", new { userId, cartId });
            return model;

        }

        [HttpDelete]
        [Route("DeleteShopCart/{cartId}")]
        public async Task<IActionResult> FullDeleteShopCartAsync(int userId, int cartId)
        {
            await _shopCartService.FullDeleteShopCartAsync(userId, cartId);
            return Ok();
        }

        #region Cart Items

        [HttpPost]
        [Route("ShopingCart/{cartId}/addItem")]
        [ModelStateFilter]
        public async Task<IActionResult> AddCartItemAsync(int userId, int cartId, Cart_Items_Entity cartItem)
        {
            if (!_shopCartService.checkIfShopCartitemExist(userId, cartId, cartItem.BookId))
            {
                await _shopCartService.AddCartItemAsync(userId, cartId, cartItem);
                var result = await _shopCartService.GetShopCartByIdAsync(userId, cartId);
                var location = _linkGanarator.GetPathByAction("GetShopCartByIdAsync", "ShopCart", new { userId, cartId });
                return Created(location, result);
            }
            else
            {
                return BadRequest("this item already exists in user's shopping cart");
            }
        }


        [HttpPut]
        [Route("ShopingCart/{cartId}/EditItem/{itemId}")]
        public async Task<ActionResult<Cart_Items_Entity>> EditCartItemAsync(int userId, int cartId, int itemId, Cart_Items_Entity cartItem)
        {

            //if (_userRolesService.CheckIfNewUserRoleExist(userRole))
            //{
            var model = await _shopCartService.EditCartItemAsync(userId, cartId, itemId, cartItem);
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            var location = _linkGanarator.GetPathByAction("GetShopCartByIdAsync", "ShopCart", new { userId, cartId });
            return model;
        }
        //else
        //{
        //    return BadRequest("user Role has already been created");
        //}

        [HttpDelete]
        [Route("ShopingCart/{cartId}/DeleteItems")]
        public async Task<IActionResult> DeleteCartItemAsync([FromBody] int[] itemsIds)
        {
            if (itemsIds != null)
            {
                await _shopCartService.DeleteCartItemAsync(itemsIds);
                return Ok();
            }
            else
            {
                return BadRequest("items is not checked");
            }
        }
        #endregion
    }
}


