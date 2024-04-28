using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data.Config
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(
                    Ostatus => Ostatus.ToString(),
                    Ostatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), Ostatus)
                    );

            //builder.HasOne(O => O.deliveryMethod).WithMany();
            //or
            //builder.HasIndex(O => O.deliveryMethod).IsUnique();


            builder.Property(O => O.Subtotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o=>o.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);    
        }

    }
}
