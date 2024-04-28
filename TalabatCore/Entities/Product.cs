using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; } //Navigational Property =>One
        public int BrandId { get; set; }//Navigational Property =>One

        public ProductBrand Brand { get; set; }
        public ProductCategory Category { get; set; }

    }
}
