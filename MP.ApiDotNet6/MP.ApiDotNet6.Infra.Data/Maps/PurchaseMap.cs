﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Compra");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("IdCompra")
                .UseIdentityColumn();
            builder.Property(x => x.PersonId)
                .HasColumnName("IdPessoa");
            builder.Property(x => x.ProductId)
                .HasColumnName("IdProduto");
            builder.Property(x => x.Date)
                .HasColumnName("DataCompra");
            builder.HasOne(x => x.Person)
                .WithMany(p => p.Purchases)
                .HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Product)
                .WithMany(p => p.Purchases)
                .HasForeignKey(x => x.ProductId);
        }
    }
}