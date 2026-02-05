using Microsoft.AspNetCore.Mvc;

using Nadam.WarehouseManagement.Contracts;

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
}
