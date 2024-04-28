using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    public class BasketController: BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        //lets start EndPoints

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return Ok(basket?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async  Task<ActionResult<CustomerBasket>> updateBasket (CustomerBasketDTOs basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTOs,CustomerBasket>(basket);

            var updatedBasket =  await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (updatedBasket == null) return BadRequest(new APIResponse(400));
            return Ok(updatedBasket);
        }


        [HttpDelete]
        public async Task DeleteBasket(string basketId)
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
