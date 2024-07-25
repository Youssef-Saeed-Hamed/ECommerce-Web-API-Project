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
	internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.OwnsOne(o => o.Product, o => o.WithOwner());
			builder.Property(o => o.Price).HasColumnType("decimal(18,5)");
		}
	}
}
