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
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly LinkGenerator _linkGanarator;
        private readonly ILoggerService _logger;


        public BooksController(IBookService bookService, LinkGenerator linkGanarator, ILoggerService logger)
        {
            _bookService = bookService;
            _linkGanarator = linkGanarator;
            _logger = logger;
        }

        [HttpGet]
        [Route("AllBooks")]
        public async Task<IActionResult> GetAllBooksAsync()
        {

            var model = await _bookService.GetAllBooksAsync();
            //var result = _mapper.Map<CatalogsEntity>(catalogs);
            return Ok(model);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync(int bookId, bool includeCatalogs = true, bool includeSupplier = true)
        {

            var model = await _bookService.GetBookByIdAsync(bookId, includeCatalogs, includeSupplier);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [HttpPost]
        [Route("CreateBook")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateBookAsync(BooksEntity book)
        {
            if (_bookService.CheckIfNewBookExit(book))
            {
                var model = await _bookService.CreateBookAsync(book);
                var location = _linkGanarator.GetPathByAction("GetBookByIdAsync", "Books", new { bookId = model.BookId });
                return Created(location, model);
            }
            else
            {
                return BadRequest("book has already been created");
            }
        }

        [HttpPut]
        [Route("EditBook/{bookId}")]
        public async Task<ActionResult<BooksEntity>> EditBookAsync(int bookId, BooksEntity book)
        {

            if (_bookService.CheckIfBookExit(bookId, book))
            {
                var model = await _bookService.EditBookAsync(bookId, book);
                //var result = _mapper.Map<CatalogsEntity>(catalogs);
                var location = _linkGanarator.GetPathByAction("GetBookByIdAsync", "Books", new { bookId = model.BookId });
                return model;
            }
            else
            {
                return BadRequest("catalog has already been created");
            }
        }
        [HttpDelete]
        [Route("DeleteBook/{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            await _bookService.DeleteBookAsync(bookId);
            return Ok();
        }

    }
}