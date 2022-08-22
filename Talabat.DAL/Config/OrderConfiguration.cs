using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.DAL.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, Address => Address.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(
                Ostatus => Ostatus.ToString(),
                Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
