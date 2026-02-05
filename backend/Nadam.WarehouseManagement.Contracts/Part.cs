namespace Nadam.WarehouseManagement.Contracts;

/// <summary>
/// Represents a Part in the warehouse
/// </summary>
/// <param name="Id">The unique Id of the part.</param>
/// <param name="Name">The name of the part.</param>
/// <param name="Description">Short description of the part.</param>
/// <param name="Price">Current price in HUF of the part.</param>
/// <param name="Weight">Weight of the part represented in Kg.</param>
/// <param name="Amount">The current amount of this part in the warehouse.</param>
public record Part(
    Guid Id,
    string Name,
    string Description,
    uint Price,
    uint Weight,
    uint Amount);
