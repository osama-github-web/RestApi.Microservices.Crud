using System.ComponentModel.DataAnnotations.Schema;

namespace CategoryApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public int ProductId { get; set; }
        [NotMapped]
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
