using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class orderService : IOrderService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public orderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, Address shippingAddress)
        {
            //Get Basket from Basket Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);


            //Get Selected Items at Basket from Products Repo
            var orderItems = new List<OrderItem>();
            if(basket?.Items?.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var products = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var ProductItemOrdered =  new ProductItemOrdered(item.Id , products.Name , products.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrdered, products.Price, item.Quantity);

                    orderItems.Add(orderItem);


                }

            }
         
      
            //Calculate Subtotal
            var subTotal = orderItems.Sum(OrderItem => OrderItem.Price * OrderItem.Quantity);
            //Get DeliveryMethod from  DeliveryMethods
            var delivery = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethod);   
            //Create Order
            var Order = new Order(buyerEmail,shippingAddress,delivery,orderItems,subTotal);
            await _unitOfWork.Repository<Order>().AddAsync(Order);
            //Save Database => de elly ht5lenii 23ml Unit of Work asln
             var result =   await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return Order;

        }


        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
