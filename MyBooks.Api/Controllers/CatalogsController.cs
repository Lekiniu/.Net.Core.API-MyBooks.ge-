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
    public class CatalogsController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;


        public CatalogsController(ICatalogService catalogService, LinkGenerator linkGanarator, ILoggerService logger)
        {
            _catalogService = catalogService;
            _linkGanarator = linkGanarator;
            _logger = logger;
        }




        [HttpGet]
        [Route("AllCatalogs")]
        public async Task<IActionResult> GetAllCatalogsAsync()
        {

                var model = await _catalogService.GetAllCatalogsAsync();
                //var result = _mapper.Map<CatalogsEntity>(catalogs);
                return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogByIdAsync(int id, bool includeBooks = true)
        {

                var model = await _catalogService.GetCatalogByIdAsync(id, includeBooks);
                if (model == null) return NotFound();
                return Ok(model);
        }
        [HttpPost]
        [Route("CreateCatalog")]
         [ModelStateFilter]
        public async Task<IActionResult> CreateCatalogAsync(CatalogsEntity catalog)
        {
         
                    if (!_catalogService.CheckIfCatalogExit(catalog))
                    {
                        var model = await _catalogService.CreateCatalogAsync(catalog);
                        var location = _linkGanarator.GetPathByAction("GetCatalogByIdAsync", "Catalogs", new { id = model.CatalogId });
                        return Created(location, model);
                    }
                    else
                    {
                        return BadRequest("catalog has already been created");
                    }
            }                    

        [HttpPut]
        [Route("EditCatalog/{catalogId}")]
        public async Task<ActionResult<CatalogsEntity>> EditCatalogAsync(int catalogId, CatalogsEntity catalog)
        {

                if (!_catalogService.CheckIfCatalogExit(catalog))
                {
                    var model = await _catalogService.EditCatalogAsync(catalogId, catalog);
                    //var result = _mapper.Map<CatalogsEntity>(catalogs);
                    var location = _linkGanarator.GetPathByAction("GetCatalogByIdAsync", "Catalogs", new { id = model.CatalogId });
                    return model;
                }
                else
                {
                    return BadRequest("catalog has already been created");
                }
            }
        [HttpDelete]
        [Route("DeleteCatalog/{catalogId}")]
        public async Task<IActionResult> DeleteCatalogAsync(int catalogId)
        {

                await _catalogService.DeleteCatalogAsync(catalogId);
                return Ok();
        }

        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            //_logger.LogError($"Something went wrong: {StatusCodes.Status500InternalServerError}");
            //return StatusCode(StatusCodes.Status500InternalServerError);
            //const string msg = "Unable to PUT license creation request";
            //return StatusCode((int)HttpStatusCode.InternalServerError, msg);
            throw new System.ArgumentException();
        }
    }
}