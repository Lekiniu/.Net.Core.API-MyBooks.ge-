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
   public class CatalogService : ICatalogService
    {
        private readonly MyBooksDBContext _context;
       
        public CatalogService(MyBooksDBContext context)
        {
            _context = context;
        }

        public bool CheckIfCatalogExit(CatalogsEntity catalog)
        {
            return _context.Catalogs.FirstOrDefault(e => e.Name == catalog.Name) != null ? true : false;
         }


    public async Task<IEnumerable<CatalogsEntity>> GetAllCatalogsAsync()
        {
            
            var catalogsModel = await _context.Catalogs.
                 Include(m => m.Catalogs_Books)
                    .ThenInclude(m => m.Book).ToListAsync();

            var result = Mapping.Mapper.Map<IEnumerable<CatalogsEntity>>(catalogsModel);
            return result;
        }

        public async Task<CatalogsEntity> GetCatalogByIdAsync(int catalogid, bool includeBooks)
        {
            var catalogModel = await _context.Catalogs.FirstOrDefaultAsync(m => m.CatalogId == catalogid);

            if (includeBooks) { 
                catalogModel = await _context.Catalogs
                    .Include(m => m.Catalogs_Books)
                    .ThenInclude(m => m.Book)
                    //.ThenInclude(m=>m.Supplier)
                    .FirstOrDefaultAsync(m => m.CatalogId == catalogid);
            }

            //var model = _context.Catalogs_Books.Where(j => j.Catalog.CatalogId == catalogModel.CatalogId).Select(y => y.Book).ToList();
            
            var result = Mapping.Mapper.Map<CatalogsEntity>(catalogModel);
            
            return  result;
        }

        public async Task<CatalogsEntity> CreateCatalogAsync (CatalogsEntity catalog)
        {

                var result = Mapping.Mapper.Map<Catalogs>(catalog);
                await _context.AddAsync(result);
                await _context.SaveChangesAsync();

                return Mapping.Mapper.Map<CatalogsEntity>(result);

        }

        public async Task<CatalogsEntity> EditCatalogAsync(int catalogId, CatalogsEntity catalog)
        {
            var oldmodel = await _context.Catalogs
                .FirstOrDefaultAsync(e => e.CatalogId == catalogId);
           
            var result = Mapping.Mapper.Map(catalog, oldmodel);
            await _context.SaveChangesAsync();

            return Mapping.Mapper.Map<CatalogsEntity>(result);
        }

        public async Task DeleteCatalogAsync(int catalogId)
        {
            var catalogsBooks = await _context.Catalogs_Books
                .Where(e => e.CatalogId == catalogId)
                .ToListAsync();

            //var books = await _context.Books
            //    .Where(e => catalogsBooks.Any(j=>e.BookId == j.BookId))
            //    .ToListAsync();

            var catalog = await _context.Catalogs
                .FirstOrDefaultAsync(e => e.CatalogId == catalogId);


            //_context.Books.RemoveRange(books);
            _context.Catalogs_Books.RemoveRange(catalogsBooks);
            _context.Catalogs.Remove(catalog);
            //var result = Mapping.Mapper.Map(catalog, oldmodel);
            await _context.SaveChangesAsync();

            //return Mapping.Mapper.Map<CatalogsEntity>(result);
        }

    }
}
