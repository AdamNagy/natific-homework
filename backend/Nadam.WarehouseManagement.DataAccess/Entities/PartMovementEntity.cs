namespace Nadam.WarehouseManagement.DataAccess.Entities;

public class PartMovementEntity
{
    public Guid PartId { get; set; }
    public bool Inbound { get; set; }
    public uint Amount { get; set; }
    public string? Reason { get; set; }
    public PartEntity Part { get; set; }
}