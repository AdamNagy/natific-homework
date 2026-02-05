namespace Nadam.WarehouseManagement.Contracts;

public record ListReslt<T>(IEnumerable<T> Items, int SumCount, bool HasMore);
