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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;


        public UsersController(IAddressService addressService,  IUserService userService, LinkGenerator linkGanarator, ILoggerService logger)
        {
            _addressService = addressService;
            _userService = userService;
            _linkGanarator = linkGanarator;
            _logger = logger;
        }

        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            var model = await _userService.GetAllUsersAsync();
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id, bool includeBooks = true)
        {

            var model = await _userService.GetUserByIdAsync(id, includeBooks);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPost]
        [Route("CreateUser")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateUserAsync(UserEntity user)
        {
            if (_userService.CheckIfNewUserExist(user))
            {
                var model = await _userService.CreateUserAsync(user);
                var location = _linkGanarator.GetPathByAction("GetUserByIdAsync", "Users", new { id = model.UserId });
                return Created(location, model);
            }
            else
            {
                return BadRequest("user has already been created");
            }
        }

        [HttpPut]
        [Route("EditUser/{userId}")]
        public async Task<ActionResult<UserEntity>> EditUserAsync(int userId, UserEntity user)
        {

            if (_userService.CheckIfUserExist(userId, user))
            {
                var model = await _userService.EditUserAsync(userId, user);
                //var result = _mapper.Map<CatalogsEntity>(catalogs);
                var location = _linkGanarator.GetPathByAction("GetUserByIdAsync", "Users", new { id = model.UserId });
                return model;
            }
            else
            {
                return BadRequest("user has already been created");
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }


        [HttpDelete]
        [Route("FullDeleteUser/{userId}")]
        public async Task<IActionResult> FullDeleteUserAsync(int userId)
        {
            await _userService.FullDeleteUserAsync(userId);
            return Ok();
        }

        #region Users Address
        //2420180
        //2421424

        [HttpGet("{userId}/Address/{addressId}")]
        public async Task<IActionResult> GetUserAddressByIdAsync(int userId, int addressId)
        {

            var model = await _addressService.GetUserAddressByIdAsync(userId, addressId);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPost]
        [Route("{userId}/CreateAddress")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateUserAddressAsync(int userId, AddressEntity address)
        {

            //if (_addressService.CheckIfAddressSupplierExist(supplierId))
            //{
            var model = await _addressService.CreateUserAddressAsync(userId, address);
            var location = _linkGanarator.GetPathByAction("GetUserAddressByIdAsync", "Users", new { userId = model.UsesId, addressId = model.AddressId });
            return Created(location, model);

            //else
            //{
            //    return BadRequest("Suppliers does not exist");
            //}
        }

        [HttpPut]
        [Route("{userId}/EditAddress/{addressId}")]
        [ModelStateFilter]
        public async Task<IActionResult> EditUserAddressAsync(int userId, int addressId, AddressEntity address)
        {

            //if (_addressService.CheckIfAddressSupplierExist(supplierId))
            //{
            var model = await _addressService.EditUserAddressAsync(userId, addressId, address);
            var location = _linkGanarator.GetPathByAction("GetUserAddressByIdAsync", "Users", new { userId, addressId });
            return Created(location, model);

            //else
            //{
            //    return BadRequest("Suppliers does not exist");
            //}
        }

        [HttpDelete]
        [Route("{userId}/DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteUserAddressAsync(int userId, int addressId)
        {
            await _addressService.DeleteUserAddressAsync(userId, addressId);
            return Ok();
        }
        #endregion



 

    }
}