using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.APIs.DTOS
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethod { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
