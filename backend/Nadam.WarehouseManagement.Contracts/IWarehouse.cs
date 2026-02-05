namespace Nadam.WarehouseManagement.Contracts;

public interface IWarehouse
{
    Task<ListReslt<Part>> List(int skip, int take);

    Task Add(Part part);
    Task Update(Part part);
    Task Delete(Guid id);

    Task IncriseAmount(Guid partId, uint amount);
    Task DecreaseAmount(Guid partId, uint amount);

    // Statisztikák:
    // Raktár össztömege
    Task<uint> GetSumWeight();
    // Raktár összértéke
    Task<uint> GetSumPrize();
    // Legtöbb tételű termék
    // Legnagyobb tömegű termék
}
