namespace Nadam.WarehouseManagement.DataAccess.Entities;

public class PartEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public uint Price { get; set; }
    public uint Weight { get; set; }
    public uint Amount { get; set; }
    public ICollection<PartMovementEntity>? Movements { get; set; }
}
