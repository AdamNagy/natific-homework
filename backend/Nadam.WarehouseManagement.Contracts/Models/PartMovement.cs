namespace Nadam.WarehouseManagement.Contracts.Models;

public record PartMovement(Guid PartId, bool Inbound, int Amount, string? Reason, DateTimeOffset ShipmentDate);