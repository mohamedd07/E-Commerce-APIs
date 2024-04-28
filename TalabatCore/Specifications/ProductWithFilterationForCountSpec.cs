﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCountSpec:BaseSpecifications<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams specParams) : base(p =>
                (string.IsNullOrEmpty(specParams.Search) || (p.Name != null && p.Name.ToLower().Contains(specParams.Search))) &&
                (!specParams.brandId.HasValue || p.BrandId == specParams.brandId.Value)
               && (!specParams.categoryId.HasValue || p.CategoryId == specParams.categoryId.Value))

         {


        }
    }
}
