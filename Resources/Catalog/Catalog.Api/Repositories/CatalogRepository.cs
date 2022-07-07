using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository:ICatalogRepository
    {
        #region Constructor

        private readonly ICatalogContext _context;

        public CatalogRepository(ICatalogContext context)
        {
            _context = context;
        }

        #endregion

        #region ProductRepo

         public async Task<IEnumerable<Product>> GetProducts()
         {
             return await _context.Products.Find(x => true).ToListAsync();
         }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNAme(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);
            return await _context.Products.Find(filter).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var update = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return update.IsAcknowledged && update.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var delete = await _context.Products.DeleteOneAsync(filter);

            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        #endregion
       
    }
}
