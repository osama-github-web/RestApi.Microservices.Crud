using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }

    public class GetProductCategoriesDto
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
