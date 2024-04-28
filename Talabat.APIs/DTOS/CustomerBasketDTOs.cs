using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
    public class CustomerBasketDTOs
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public List<BaseketItemDTO> Items { get; set; }


    }
}
