using CategoryApi.Data;
using CategoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Services
{
    public class CategoryService
    {
        private readonly DbContextClass _dbContext;

        public CategoryService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var result = await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var result = _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var filteredData = await _dbContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (filteredData != null)
            {
                _dbContext.Categories.Remove(filteredData);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}