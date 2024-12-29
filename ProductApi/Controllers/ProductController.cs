using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _productService.GetProductsAsync();

            try
            {
                using HttpClient httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7000/api/Categories/")
                };
                HttpResponseMessage response = await httpClient.GetAsync("GetProductCategories");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var categories = await response.Content.ReadFromJsonAsync<List<GetProductCategoriesDto>>();
                    foreach (var item in products)
                    {
                        var category = GetCategory(item.ProductId, categories);
                        if (category != null)
                        {
                            item.CategoryId = category.CategoryId;
                            item.CategoryName = category.CategoryName;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return products;
        }

        private GetProductCategoriesDto? GetCategory(int productid, List<GetProductCategoriesDto>? getProductCategoriesDtos, int count = 0)
        {
            if (count == getProductCategoriesDtos.Count)
                return null;

            if (getProductCategoriesDtos[count].ProductId == productid)
            {
                return getProductCategoriesDtos[count];
            }

            count++;
            var category = GetCategory(productid, getProductCategoriesDtos, count);
            return category;
        }


        [HttpGet("GetProductById")]
        public async Task<Product> GetProductById(int Id)
        {
            return await _productService.GetProductByIdAsync(Id);
        }


        [HttpPost("AddProduct")]
        public async Task<Product> AddProduct(Product product)
        {
            var prod = await _productService.AddProductAsync(product);

            var dto = new ProductCategoryDto();
            dto.ProductId = prod.ProductId;
            dto.CategoryId = product.CategoryId;
            var jsonContent = JsonContent.Create(dto);

            using HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7001/api/Categories/")
            };

            HttpResponseMessage response = await httpClient.PostAsync("AddProductCategory", jsonContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var categoryResponse = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
            }

            return prod;
        }


        [HttpPut("UpdateProduct")]
        public async Task<Product> UpdateProduct(Product product)
        {
            var prod = await _productService.UpdateProductAsync(product);

            var dto = new ProductCategoryDto();
            dto.ProductId = prod.ProductId;
            dto.CategoryId = product.CategoryId;
            var jsonContent = JsonContent.Create(dto);

            using HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7001/api/Categories/")
            };

            HttpResponseMessage response = await httpClient.PostAsync("UpdateProductCategory", jsonContent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var categoryResponse = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
                if (categoryResponse is null)
                    return null;
            }
            return prod;
        }


        [HttpDelete("DeleteProduct")]
        public async Task<bool> DeleteProduct(int Id)
        {
            return await _productService.DeleteProductAsync(Id);
        }
    }
}
