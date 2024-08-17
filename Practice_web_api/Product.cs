using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_web_api
{
    public class Product
    {

        [Required]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Column(Order = 2)]
        [Required, MaxLength(50)]
        [StringLength(50, ErrorMessage = "The Client Id should be of 8 digits.", MinimumLength = 10)]
        public string ?Description { get; set; }

        [Required]
        [Column(Order = 3, TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
