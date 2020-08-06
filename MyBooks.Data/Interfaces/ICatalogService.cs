using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;

namespace MyBooks.Data.Interfaces
{
    public interface ICatalogService
    {

        Task<CatalogsEntity> GetCatalogByIdAsync(int id, bool includeBooks);

        //Books GetBooksByCatalogId(int id);

        Task<IEnumerable<CatalogsEntity>> GetAllCatalogsAsync();

        Task<CatalogsEntity> CreateCatalogAsync(CatalogsEntity catalog);

        Task<CatalogsEntity> EditCatalogAsync(int id, CatalogsEntity catalog);

        Task DeleteCatalogAsync(int catalogId);

        bool CheckIfCatalogExit(CatalogsEntity catalog);
    }
}
