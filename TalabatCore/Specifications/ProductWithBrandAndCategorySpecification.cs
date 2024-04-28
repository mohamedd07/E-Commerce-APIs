using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecification:BaseSpecifications<Product>
    {

        public ProductWithBrandAndCategorySpecification(ProductSpecParams specParams)
            
            :base(p=>
                 (string.IsNullOrEmpty(specParams.Search) || (p.Name != null && p.Name.ToLower().Contains(specParams.Search))) &&

                (!specParams.brandId.HasValue || p.BrandId == specParams.brandId.Value)
               && (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId.Value)
            
            )
        {
            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
                {
                    case "priceAsc":
                        SetOrderBy(P => P.Price);

                        break;
                    case "priceDesc":
                        SetOrderByDesc(P => P.Price);

                        break;

                    default: SetOrderBy(P => P.Name); break;
                }
            }
            else
                SetOrderBy(P => P.Name);


            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);

            IncludesAdd();

        }

        

        public ProductWithBrandAndCategorySpecification(int id) : base(P => P.Id == id)
        {
            IncludesAdd();
        }

        private void IncludesAdd()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
