using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;

namespace MyBooks.Data.Interfaces
{
   public interface IBookService
    {
        Task<BooksEntity> GetBookByIdAsync(int bookId, bool includeCatalogs, bool includeSupplier);

        Task<IEnumerable<BooksEntity>> GetAllBooksAsync();

        bool CheckIfBookExit(int bookId, BooksEntity book);

        bool CheckIfNewBookExit(BooksEntity book);

        Task<BooksEntity> CreateBookAsync(BooksEntity book);

        Task<BooksEntity> EditBookAsync(int bookId, BooksEntity book);

        Task DeleteBookAsync(int bookId);
    }
}
