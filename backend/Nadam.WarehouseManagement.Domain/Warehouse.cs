using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Nadam.WarehouseManagement.Contracts.Core;
using Nadam.WarehouseManagement.Contracts.Interfaces;
using Nadam.WarehouseManagement.Contracts.Models;
using Nadam.WarehouseManagement.DataAccess;
using Nadam.WarehouseManagement.DataAccess.Entities;

namespace Nadam.WarehouseManagement.Domain;

public class Warehouse : IWarehouse
{
    private readonly WarehouseDbContext _dbContext;
    private readonly ILogger<Warehouse> _logger;

    public Warehouse(WarehouseDbContext dbContext, ILogger<Warehouse> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    #region Read
    public async Task<ListReslt<Part>> List(int skip, int take)
    {
        try
        {
            var items = await _dbContext.Parts.Skip(skip).Take(take).AsNoTracking().ToListAsync();
            var sumCount = await _dbContext.Parts.CountAsync();

            return new ListReslt<Part>(true, items.Select(Map), sumCount, skip + take < sumCount, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not read data from db.");
            return new ListReslt<Part>(false, null, null, null, "Could not read data from db.");
        }
    }

    public async Task<ListReslt<PartMovement>> GetMovements(Guid partId)
    {
        try
        {
            var movements = await _dbContext.PartMovements.Where(p => p.PartId == partId).AsNoTracking().ToListAsync();
            return new ListReslt<PartMovement>(true, movements.Select(Map), movements.Count, false, null);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Could not read from db.");
            return new ListReslt<PartMovement>(false, null, 0, false, "Could not read from db.");
        }
    }
    #endregion

    #region Operations
    public async Task<Result> Add(NewPartRequest part)
    {
        try
        {
            var entity = Map(part);
            _dbContext.Parts.Add(entity);
            var changes = await _dbContext.SaveChangesAsync();

            if(changes == 1)
            {
                return new Result(true, null);
            }

            return new Result(false, "Could not inster record.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not insert new Part. {@Part}", part);
            return new Result(false, ex.Message);
        }
    }

    public async Task<Result> Update(PartPatch part)
    {
        try
        {
            var partEntity = await _dbContext.Parts.SingleAsync(x => x.Id == part.Id);

            if (part is null)
            {
                return Result.Failed($"Could not find endity with id '{part.Id}'");
            }

            partEntity.Name = part.Name;
            partEntity.Description = part.Description;
            partEntity.Price = part.Price;
            partEntity.Weight = part.Weight;

            var updates = await _dbContext.SaveChangesAsync();

            return updates == 1 ?
                Result.Success() :
                Result.Failed("Could not update entity.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update entity. {@Part}", part);
            return new Result(false, ex.Message);
        }
    }

    public async Task<Result> IncriseAmount(Guid partId, int amount)
    {
        var part = await _dbContext.Parts.FindAsync(partId);

        if (part is null)
        {
            return Result.Failed($"Could not find endity with id '{partId}'");
        }

        try
        {
            part.Amount = part.Amount + amount;
            part.Movements.Add(new PartMovementEntity()
            {
                Amount = amount,
                Inbound = true,
                PartId = partId,
                Reason = "Stock increase"
            });

            var updates = await _dbContext.SaveChangesAsync();

            if (updates == 1) { return new Result(true, null); }

            return new Result(false, "Could not update entiy.");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Could not update entity. {@Part}", part);
            return new Result(false, ex.Message);
        }
    }

    public async Task<Result> DecreaseAmount(Guid partId, int amount)
    {
        var part = await _dbContext.Parts.FindAsync(partId);

        if (part is null)
        {
            return Result.Failed($"Could not find endity with id '{partId}'");
        }

        try
        {
            var newAmount = part.Amount - amount;
            if (newAmount < 0)
            {
                return Result.Failed($"New amount can not be lower then 0.");
            }

            part.Amount = newAmount;
            var updates = await _dbContext.SaveChangesAsync();

            if (updates == 1) { return new Result(true, null); }

            return new Result(false, "Could not update entiy.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not update entity. {@Part}", part);
            return new Result(false, ex.Message);
        }
    }

    public async Task<Result> Delete(Guid id)
    {
        try
        {
            var deleted = await _dbContext.Parts.Where(p => p.Id == id).ExecuteDeleteAsync();

            if (deleted == 1)
            {
                return new Result(true, null);
            }

            return new Result(false, "Could not inster record.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not delete entity. {@Id}", id);
            return new Result(false, ex.Message);
        }
    }
    #endregion

    #region Reports
    public async Task<Result<int>> GetSumPrize()
    {
        try
        {
            var sum = await _dbContext.Parts.Select(p => new
            {
                sumPrize = p.Price * p.Amount
            }).SumAsync(q => q.sumPrize);

            return new Result<int>(true, null, sum);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Coluld not get sum.");
            return new Result<int>(false, ex.Message, 0);
        }
    }

    public async Task<Result<int>> GetSumWeight()
    {
        try
        {
            var sum = await _dbContext.Parts.SumAsync(p => p.Weight);
            return new Result<int>(true, null, sum);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Coluld not get sum.");
            return new Result<int>(false, ex.Message, 0);
        }
    }

    public async Task<Result<Part>> GetPartWithMostAmount()
    {
        try
        {
            var mostWeightedPart = await _dbContext.Parts
                .OrderByDescending(p => p.Amount)
                .FirstOrDefaultAsync();

            if(mostWeightedPart is not null)
            {
                return new Result<Part>(true, null, Map(mostWeightedPart));
            }

            return new Result<Part>(false, "Could not get report.", null);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Could not get report.");
            return new Result<Part>(false, "Could not get report.", null);
        }
    }

    public async Task<Result<Part>> GetPartWithMostWeight()
    {
        try
        {
            var mostWeightedPart = await _dbContext.Parts
                .OrderByDescending(p => (decimal)p.Weight * p.Amount)
                .FirstOrDefaultAsync();

            if (mostWeightedPart is not null)
            {
                return new Result<Part>(true, null, Map(mostWeightedPart));
            }

            return new Result<Part>(false, "Could not get report.", null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not get report.");
            return new Result<Part>(false, "Could not get report.", null);
        }
    }
    #endregion

    #region Mappers
    private static Part Map(PartEntity entity) 
        => new Part(entity.Id, entity.Name, entity.Description, entity.Price, entity.Weight, entity.Amount);

    private static PartEntity Map(NewPartRequest request)
        => new PartEntity()
        {
            Amount = 0,
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            Weight = request.Weight,
        };

    private static PartMovement Map(PartMovementEntity entity) 
        => new PartMovement(entity.Id, entity.Inbound, entity.Amount, entity.Reason, entity.ShipmentDate);
    #endregion
}
