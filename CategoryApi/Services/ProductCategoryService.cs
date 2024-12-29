using CategoryApi.Data;
using CategoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Services
{
    public class ProductCategoryService
    {
        private readonly DbContextClass _dbContext;

        public ProductCategoryService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _dbContext.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetAsync(int id)
        {
            return await _dbContext.ProductCategories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductCategory> GetByProductIdAsync(int productId)
        {
            return await _dbContext.ProductCategories.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }
        
        public async Task<List<ProductCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.ProductCategories.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<ProductCategory> AddAsync(ProductCategory productCategory)
        {
            var result = await _dbContext.ProductCategories.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProductCategory> UpdateAsync(ProductCategory productCategory)
        {
            if (productCategory.ProductId < 1 || productCategory.CategoryId < 1)
                return null;

            var prodCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(x => x.ProductId == productCategory.ProductId);
            if (prodCategory == null)
                return await AddAsync(productCategory);

            prodCategory.CategoryId = productCategory.CategoryId;
            var result = _dbContext.ProductCategories.Update(prodCategory);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var filteredData = await _dbContext.ProductCategories.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            if (filteredData != null)
            {
                _dbContext.ProductCategories.Remove(filteredData);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}