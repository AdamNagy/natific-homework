using Nadam.WarehouseManagement.Contracts.Core;
using Nadam.WarehouseManagement.Contracts.Models;

namespace Nadam.WarehouseManagement.Contracts.Interfaces;

/// <summary>
/// Represents a warehouse storage of parts. Contains basic operations for parts and its movements.
/// Also contains some reporting possibilities.
/// </summary>
public interface IWarehouse
{
    /// <summary>
    /// Lists the available parts in the warehouse in a paginated manner.
    /// </summary>
    /// <param name="skip">How many entites to skip.</param>
    /// <param name="take">How many entites to take.</param>
    /// <returns>The <see cref="ListReslt{T}"/> of <see cref="Part"/> for the current query.</returns>
    Task<ListReslt<Part>> List(int skip, int take);

    /// <summary>
    /// Adds a new part to the warehouse.
    /// </summary>
    /// <param name="part">The request for the insert.</param>
    /// <returns>A <see cref="Result"> representing the success of the operation.</returns>
    Task<Result> Add(NewPartRequest part);
    Task<Result> Update(PartPatch part);
    Task<Result> Delete(Guid id);

    Task<Result> IncriseAmount(Guid partId, int amount);
    Task<Result> DecreaseAmount(Guid partId, int amount);

    // Statisztikák:
    // Raktár össztömege
    Task<Result<int>> GetSumWeight();

    // Raktár összértéke
    Task<Result<int>> GetSumPrize();

    // Legtöbb tételű termék
    Task<Result<Part>> GetPartWithMostAmount();

    // Legnagyobb tömegű termék
    Task<Result<Part>> GetPartWithMostWeight();
}
