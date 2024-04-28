using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        public readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer )
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
           return await _database.KeyDeleteAsync( BasketId );
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
           var basket = await _database.StringGetAsync( BasketId );
            return BasketId == null ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {

            var createdOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize<CustomerBasket>(basket), TimeSpan.FromDays(30));
           if(createdOrUpdated is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
