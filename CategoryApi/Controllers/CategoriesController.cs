using CategoryApi.Dtos;
using CategoryApi.Models;
using CategoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ProductCategoryService _productCategoryService;

        public CategoriesController(CategoryService categoryService, ProductCategoryService productCategoryService)
        {
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return categories;
        }

        [HttpGet("GetById/{id}")]
        public async Task<Category> GetCategoryById(int Id)
        {
            return await _categoryService.GetByIdAsync(Id);
        }

        [HttpPost("AddCategory")]
        public async Task<Category> AddCategory(Category category)
        {
            var cat = await _categoryService.AddCategoryAsync(category);
            return cat;
        }

        [HttpPut("UpdateCategory")]
        public async Task<Category> UpdateCategory(Category category)
        {
            return await _categoryService.UpdateCategoryAsync(category);
        }

        [HttpPost("AddProductCategory")]
        public async Task<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            return await _productCategoryService.AddAsync(productCategory);
        }

        [HttpPost("UpdateProductCategory")]
        public async Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory)
        {
            return await _productCategoryService.UpdateAsync(productCategory);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<bool> DeleteCategory(int Id)
        {
            return await _categoryService.DeleteCategoryAsync(Id);
        }

        [HttpGet("Product/{productId}")]
        public async Task<Category> GetProductCategory(int productId)
        {
            var productCategory = await _productCategoryService.GetByProductIdAsync(productId);
            var category = await _categoryService.GetByIdAsync(productCategory.CategoryId);
            return category;
        }


        [HttpGet("GetProductCategories")]
        public async Task<List<GetProductCategoriesDto>> GetProductCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var getProductCategoriesDto = new List<GetProductCategoriesDto>();
            await AddProductIdToCategory(categories, getProductCategoriesDto);
            return getProductCategoriesDto;
        }

        private async Task AddProductIdToCategory(List<Category> categories, List<GetProductCategoriesDto> getProductCategoriesDtos, int count = 0)
        {
            if (count == categories.Count)
                return;

            if (count != categories.Count)
            {
                var productCategories = await _productCategoryService.GetByCategoryIdAsync(categories[count].Id);
                if (productCategories != null)
                {
                    productCategories.ForEach(x =>
                    {
                        var getProductCategoriesDto = new GetProductCategoriesDto();
                        getProductCategoriesDto.CategoryId = x.CategoryId;
                        getProductCategoriesDto.ProductId = x.ProductId;
                        getProductCategoriesDto.CategoryName = categories[count].Name;
                        getProductCategoriesDtos.Add(getProductCategoriesDto);
                    });
                }
                count++;
                await AddProductIdToCategory(categories, getProductCategoriesDtos, count);
            }

            return;
        }
    }
}