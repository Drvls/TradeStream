using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeStream.Domain.Entities;

namespace TradeStream.Infra.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderDate).IsRequired().HasColumnType("datetime2");
        builder.Property(o => o.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
        builder.Property(o => o.AssetCode).IsRequired().HasMaxLength(6);
        builder.Property(o => o.AssetName).IsRequired().HasMaxLength(50);
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
        builder.Property(o => o.TargetValue).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.Quantity).IsRequired().HasColumnType("int");
        builder.Property(o => o.OrderType).IsRequired().HasConversion<string>().HasMaxLength(5);
    }
}