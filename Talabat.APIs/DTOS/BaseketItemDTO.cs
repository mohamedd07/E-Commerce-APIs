using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
    public class BaseketItemDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]

        public string pictureUrl { get; set; }
        [Required]

        public string Category { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage =" Price must be Greater than Zero")]

        public decimal Price { get; set; }
        [Required]

        [Range(1,int.MaxValue , ErrorMessage ="Quantity Must be at least one Quantity")]
        public int Quantity { get; set; }
    }
}