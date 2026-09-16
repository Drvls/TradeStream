using Microsoft.EntityFrameworkCore;
using TradeStream.Domain.Entities;

namespace TradeStream.Infra.Database;

public class TradeDbContext(DbContextOptions<TradeDbContext> options) : DbContext(options)
{ 
    public DbSet<Order> Orders { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Position> Positions  { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradeDbContext).Assembly);
    }
}