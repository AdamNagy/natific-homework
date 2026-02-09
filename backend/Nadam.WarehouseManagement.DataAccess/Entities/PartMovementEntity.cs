namespace Nadam.WarehouseManagement.DataAccess.Entities;

public class PartMovementEntity
{
    public Guid Id { get; set; }
    public Guid PartId { get; set; }
    public bool Inbound { get; set; }
    public int Amount { get; set; }
    public DateTimeOffset ShipmentDate { get; set; }
    public string? Reason { get; set; }
    public PartEntity? Part { get; set; }
}