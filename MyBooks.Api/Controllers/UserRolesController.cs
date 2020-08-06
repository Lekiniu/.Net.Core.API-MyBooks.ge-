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
    public class UserRolesController : Controller
    {

        private readonly IUserRolesService _userRolesService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;



        public UserRolesController( IUserRolesService userRolesService,  LinkGenerator linkGanarator, ILoggerService logger)
        {
            _linkGanarator = linkGanarator;
            _logger = logger;
            _userRolesService = userRolesService;
        }


        #region UserRoles
        [HttpGet]
        [Route("AllUserRoles")]
        public async Task<IActionResult> GetAllUserRolesAsync()
        {

            var model = await _userRolesService.GetAllUserRolesAsync();
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            return Ok(model);
        }

        [HttpGet("UserRoles/{userRoleId}")]
        public async Task<IActionResult> GetUserRolesByIdAsync(int userRoleId, bool includeUsers = true)
        {

            var model = await _userRolesService.GetUserRolesByIdAsync(userRoleId, includeUsers);
            if (model == null) return NotFound();
            return Ok(model);
        }


        [HttpPost]
        [Route("CreateUserRole")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateUserRolesAsync(UserRolesEntity userRole)
        {

            if (_userRolesService.CheckIfNewUserRoleExist(userRole))
            {
                var model = await _userRolesService.CreateUserRolesAsync(userRole);
                var location = _linkGanarator.GetPathByAction("GetUserRolesByIdAsync", "Users", new { userRoleId = model.UserRoleId });
                return Created(location, model);
            }
            else
            {
                return BadRequest("user Role has already been created");
            }
        }

        [HttpPut]
        [Route("EditUserRole/{userRoleId}")]
        public async Task<ActionResult<UserRolesEntity>> EditUserRolesAsync(int userRoleId, UserRolesEntity userRole)
        {

            if (_userRolesService.CheckIfNewUserRoleExist(userRole))
            {
                var model = await _userRolesService.EditUserRolesAsync(userRoleId, userRole);
                //var result = _mapper.Map<CatalogsEntity>(catalogs);
                var location = _linkGanarator.GetPathByAction("GetUserRolesByIdAsync", "Users", new { userRoleId = model.UserRoleId });
                return model;
            }
            else
            {
                return BadRequest("user Role has already been created");
            }
        }

        //[HttpDelete]
        //[Route("DeleteUser/{userId}")]
        //public async Task<IActionResult> DeleteUserRoleAsync(int userId)
        //{
        //    await _userService.DeleteUserAsync(userId);
        //    return Ok();
        //}
        #endregion
    }
}