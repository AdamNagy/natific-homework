using Microsoft.AspNetCore.Mvc;

using Nadam.WarehouseManagement.Contracts.Interfaces;
using Nadam.WarehouseManagement.Contracts.Models;

namespace Nadam.WarehouseManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartsController : ControllerBase
{
    private readonly IWarehouse _warehouse;

    public PartsController(IWarehouse warehouse)
    {
        _warehouse = warehouse;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] int skip, [FromQuery] int take)
    {
        var result = await _warehouse.List(skip, take);
        return Ok(result);
    }

    [HttpGet]
    [Route("seed")]
    public async Task<IActionResult> SeedData()
    {
        var items = await _warehouse.List(0, 10);

        if(items.SumCount > 0)
        {
            return Ok("Nothing to do.");
        }

        var rnd = new Random();
        for(var i = 1; i <= 10; ++i)
        {
            await _warehouse.Add(new NewPartRequest($"item {i}", $"item {i}", rnd.Next(30) * 1000, rnd.Next(10)));
        }

        return Ok("Data has been seeded.");
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] NewPartRequest request)
    {
        var result = await _warehouse.Add(request);
        return Ok(result);
    }


}
