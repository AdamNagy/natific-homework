using System.ComponentModel.DataAnnotations;

namespace Nadam.WarehouseManagement.DataAccess.Entities;

public class PartEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Weight { get; set; }
    public int Amount { get; set; }
    public ICollection<PartMovementEntity> Movements { get; set; } = [];
}
