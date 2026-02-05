namespace Nadam.WarehouseManagement.Contracts;

internal record PartMovement(Guid PartId, bool Inbound, int Amount, string Reason);