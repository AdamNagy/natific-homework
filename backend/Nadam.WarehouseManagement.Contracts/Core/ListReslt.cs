namespace Nadam.WarehouseManagement.Contracts.Core;

/// <summary>
/// Represents a paginated result for a query.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
/// <param name="IsSuccess">Indicates weather the operation was a success or not.</param>
/// <param name="Items">The actual items.</param>
/// <param name="SumCount">The total count of available items.</param>
/// <param name="HasMore">Indicator weather there are more items available.</param>
/// <param name="Error">If the operation failed, error contains user friendly message, not the <see cref="Exception"/> message.</param>
public record ListReslt<T>(bool IsSuccess, IEnumerable<T>? Items, int? SumCount, bool? HasMore, string? Error);
