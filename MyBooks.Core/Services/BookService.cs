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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MyBooks.Core.Services
{
    public class BookService : IBookService
    {
        private readonly MyBooksDBContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public BookService(MyBooksDBContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        private Books GetBook(int bookId)
        {
            return _context.Books.FirstOrDefault(e => e.BookId == bookId);
        }

        public bool CheckIfNewBookExit(BooksEntity book)
        {

            return _context.Books.FirstOrDefault(e => e.Title == book.Title) == null  ? true : false;
        }

        public bool CheckIfBookExit(int bookId, BooksEntity book)
        {

            var result = GetBook(bookId);
            return _context.Books.FirstOrDefault(e => e.Title == book.Title) == null || result.Title == book.Title ? true : false;
        }

        private async Task addCatalogsAsync(Books model, List<int>catalogIds)
        {
            foreach (var catalog in catalogIds) {
                await _context.Catalogs_Books.AddAsync(
                    new Catalogs_Books
                    {
                        CatalogId=catalog,
                        BookId= model.BookId
                    });
                    }
        }

        public async Task<IEnumerable<BooksEntity>> GetAllBooksAsync()
        {

            var booksModel = await _context.Books
                //.Include(m => m.Catalogs_Books)
                //    .ThenInclude(m => m.Catalog)
                //    .Include(m => m.User)
                .ToListAsync();

            var result = Mapping.Mapper.Map<IEnumerable<BooksEntity>>(booksModel);
            return result;
        }

        public async Task<BooksEntity> GetBookByIdAsync(int bookId, bool includeCatalogs, bool includeSupplier)
        {
           var  bookModel = new Books();

            if (includeCatalogs && includeSupplier)
            {
                bookModel = await _context.Books
                    //.Include(m => m.Supplier)
                    .Include(m => m.Catalogs_Books)
                    .ThenInclude(m => m.Catalog)
                    .FirstOrDefaultAsync(m => m.BookId == bookId);
            }
           else if (includeCatalogs)
            {
                bookModel= await _context.Books
                    .Include(m => m.Catalogs_Books)
                    .ThenInclude(n=>n.Catalog)
                    .FirstOrDefaultAsync(m => m.BookId == bookId);
            }
          else if (includeSupplier)
            {
                bookModel = await _context.Books
                    //.Include(m=>m.Supplier)
                    .FirstOrDefaultAsync(m => m.BookId == bookId);
            }
            else
            {
                bookModel = await _context.Books
                    .FirstOrDefaultAsync(m => m.BookId == bookId);
            }
                
            var result = Mapping.Mapper.Map<BooksEntity>(bookModel);

            return result;
        }

        public async Task<BooksEntity> CreateBookAsync(BooksEntity book)
        {
            var result = Mapping.Mapper.Map<Books>(book);
            await _context.Books.AddAsync(result);

            var catalogIds = book.Catalogs.Select(e => e.CatalogId).ToList();
            await addCatalogsAsync(result, catalogIds);

            await _context.SaveChangesAsync();
           var bookModel = await _context.Books
                             //.Include(m => m.Supplier)
                             .Include(m => m.Catalogs_Books)
                             .ThenInclude(m => m.Catalog)
                             .FirstOrDefaultAsync(m => m.BookId == result.BookId);

            return Mapping.Mapper.Map<BooksEntity>(result);

        }
        public async Task<BooksEntity> EditBookAsync(int bookId, BooksEntity book)
        {
            var oldmodel = await _context.Books
                .FirstOrDefaultAsync(e => e.BookId == bookId);

            var result = Mapping.Mapper.Map(book, oldmodel);
            await EditBookCatalogsAsync(bookId, book);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<BooksEntity>(result);
        }



        private async Task EditBookCatalogsAsync(int bookId, BooksEntity book)
        {
            var bookModel =  await _context.Catalogs_Books.Where(e => e.BookId == bookId).ToListAsync();

            var unselectedCatalogs = bookModel.Where(f => !book.Catalogs.Select(e=>e.CatalogId).ToList().Contains(f.CatalogId)).ToList();
            if (unselectedCatalogs.Count() != 0)
            {
                foreach (var item in unselectedCatalogs)
                {
                    _context.Catalogs_Books.RemoveRange(item);
                }
            }
            var catalogIds = bookModel.Select(e => e.CatalogId).ToList();
            var newCatalogs = book.Catalogs.Select(e => e.CatalogId).Where(catalogId => catalogIds.All(catId => catId != catalogId));


            foreach (var catalogId in newCatalogs)
            {
                       await _context.Catalogs_Books.AddAsync(
                       new Catalogs_Books()
                       {
                           CatalogId = catalogId,
                           BookId = bookId
                       });
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var catalogsBooks = await _context.Catalogs_Books
                .Where(e => e.BookId == bookId)
                .ToListAsync();

            var book = await _context.Books
                          .FirstOrDefaultAsync(e => e.BookId == bookId);



            _context.Catalogs_Books.RemoveRange(catalogsBooks);
            _context.Books.Remove(book);
            //var result = Mapping.Mapper.Map(catalog, oldmodel);
            await _context.SaveChangesAsync();

            //return Mapping.Mapper.Map<CatalogsEntity>(result);
        }

        //End of Crud

        //function to add and edit images 
        //private void AddFiles(int bookId, List<IFormFile> files)
        //{
        //    Guid g = Guid.NewGuid();
        //    string GuidString = Convert.ToBase64String(g.ToByteArray());
        //    GuidString = GuidString.Replace("=", "");
        //    GuidString = GuidString.Replace("+", "");
        //    GuidString = GuidString.Replace("/", "");

        //    foreach (var file in files)
        //    {
        //        if (file != null)
        //        {
        //            string FileName = bookId + GuidString + file.FileName;
        //            string filePath = @"\Files\Images\";
        //            var path = Path.Combine(_appEnvironment.WebRootPath + filePath + FileName);
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            _context.Files.Add(new Files()
        //            {
        //                Name = file.FileName,
        //                Url = @"/Files/Images/" + FileName,
        //                BookId = bookId,
        //                IsMain = false
        //            });
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    _context.SaveChanges();
        //}
    }
}
