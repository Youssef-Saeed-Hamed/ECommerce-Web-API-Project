using E_Commerce_Website_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(product => product.Brand)
                .WithMany().HasForeignKey(product => product.BrandId);

            builder.HasOne(product => product.ProductType)
                .WithMany().HasForeignKey(product => product.TypeId);

        }
    }
}
