using Microsoft.EntityFrameworkCore;

using Nadam.WarehouseManagement.DataAccess.Entities;

namespace Nadam.WarehouseManagement.DataAccess;

public class WarehouseDbContext : DbContext
{
    public DbSet<PartEntity> Parts { get; set; }
    public DbSet<PartMovementEntity> PartMovements { get; set; }

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
        
    }
}
