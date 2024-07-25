using E_Commerce_Website_Core.Models.Order_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Configuration
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasMany(o => o.orderItems)
				.WithOne().OnDelete(DeleteBehavior.Cascade);

			builder.OwnsOne(o => o.ShippingAddress, o => o.WithOwner());

			builder.Property(o => o.SubTotal).HasColumnType("decimal(18,5)");
		}
	}
}
