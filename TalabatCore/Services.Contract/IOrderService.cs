using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(int OrderId, string buyerEmail);
    }
}
