using Microsoft.EntityFrameworkCore;

using Nadam.WarehouseManagement.Contracts;
using Nadam.WarehouseManagement.DataAccess;
using Nadam.WarehouseManagement.DataAccess.Entities;

namespace Nadam.WarehouseManagement.Domain;

public class Warehouse : IWarehouse
{
    private readonly WarehouseDbContext _dbContext;

    public Warehouse(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ListReslt<Part>> List(int skip, int take)
    {
        var items = await _dbContext.Parts.Skip(skip).Take(take).ToListAsync();
        var sumCount = await _dbContext.Parts.CountAsync();

        return new ListReslt<Part>(items.Select(Map), sumCount, skip + take < sumCount);
    }

    public Task Add(Part part)
    {
        throw new NotImplementedException();
    }

    public Task DecreaseAmount(Guid partId, uint amount)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<uint> GetSumPrize()
    {
        throw new NotImplementedException();
    }

    public Task<uint> GetSumWeight()
    {
        throw new NotImplementedException();
    }

    public Task IncriseAmount(Guid partId, uint amount)
    {
        throw new NotImplementedException();
    }

    public Task Update(Part part)
    {
        throw new NotImplementedException();
    }

    private static Part Map(PartEntity entity) 
        => new Part(entity.Id, entity.Name, entity.Description, entity.Price, entity.Weight, entity.Amount);
}
