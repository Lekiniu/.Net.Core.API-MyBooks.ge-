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
    public class AddressesController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;

        public AddressesController(IAddressService addressService, LinkGenerator linkGanarator, ILoggerService logger)
        {
             _addressService = addressService;
            _linkGanarator = linkGanarator;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("AllAddresses")]
        //public async Task<IActionResult> GetAllAddressAsync( bool includeBooks = true)
        //{

        //    var model = await _addressService.GetAllAddressAsync(includeBooks);
        //    //var result = _mapper.Map<CatalogsEntity>(catalogs);
        //    return Ok(model);
        //}
      


    }
}