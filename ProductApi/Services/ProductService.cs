using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;

        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var result = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteProductAsync(int Id)
        {
            var filteredData = await _dbContext.Products.Where(x => x.ProductId == Id).FirstOrDefaultAsync();
            if (filteredData != null)
            {
                _dbContext.Products.Remove(filteredData);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}