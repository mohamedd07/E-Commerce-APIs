using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        public int? brandId { get; set; }
        public int? categoryId   { get; set; }
        public string? sort { get; set; }

        private int pageSize = 5 ;
        private const int MaxSize = 10;

        public int PageSize 
        {
            get { return pageSize ;}
            set { pageSize = value > MaxSize ? MaxSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        private String? search;

        public String? Search
        {
            get { return search?.ToLower(); }
            set { search = value?.ToLower(); }
        }
    }

}
