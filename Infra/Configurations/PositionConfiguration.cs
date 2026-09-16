using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeStream.Domain.Entities;

namespace TradeStream.Infra.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.AssetId).IsRequired();
        builder.Property(p => p.Quantity).IsRequired().HasColumnType("int");
        builder.HasOne(p => p.User)
            .WithMany(u => u.Positions)
            .HasForeignKey(p => p.UserId);
        builder.HasOne(p => p.Asset)
            .WithMany()
            .HasForeignKey(p => p.AssetId);
    }
}