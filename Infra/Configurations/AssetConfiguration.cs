using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeStream.Domain.Entities;

namespace TradeStream.Infra.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("assets");
        builder.HasKey(a => a.AssetId);
        builder.Property(a => a.Code).IsRequired().HasMaxLength(6);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Price).HasPrecision(18, 2);
        builder.Property(a => a.Quantity).IsRequired().HasColumnType("int");
        builder.Property(a => a.PriceHistory)
            .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<List<decimal>>(v, JsonSerializerOptions.Default)
                )
            .HasColumnType("nvarchar(max)")
            .Metadata.SetValueComparer(new ValueComparer<List<decimal>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            ));;
    }
}