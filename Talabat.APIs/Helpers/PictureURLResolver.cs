using AutoMapper;
using Talabat.APIs.DTOS;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class PictureURLResolver : IValueResolver<Product, ProductToReturnDTOs, string?>
    {
        private readonly IConfiguration _configuration;

        public PictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? Resolve(Product source, ProductToReturnDTOs destination, string? destMember, ResolutionContext context)
        {
          if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["APIBaseURl"]}/{source.PictureUrl}";

            }
          return string.Empty;
        }
    }
}
