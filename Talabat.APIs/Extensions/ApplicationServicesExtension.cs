using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Services.Contract;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static  IServiceCollection AddApplicationServices( this IServiceCollection services)
        {

            services.AddScoped(typeof(IOrderService), typeof(orderService));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
  
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.Configure<ApiBehaviorOptions>(options =>options.InvalidModelStateResponseFactory = (actioncontext) =>
            {
                var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0).SelectMany(p => p.Value.Errors).Select(E => E.ErrorMessage).ToList();
                var ValidationErrorResponse = new APIValidationErrorResponse()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(ValidationErrorResponse);

            }); //Da byt3ml marrah wa7da Per Project


            return services;
        }
    }
}
