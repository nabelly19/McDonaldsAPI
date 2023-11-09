using System;
using System.Threading.Tasks;
using McDonaldsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace McDonaldsAPI.Controllers;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase
{
    [HttpGet("create/{storeId}")]
    public async Task<ActionResult> CreateOrder(int storeId, [FromServices]IOrderRepository repo)
    {
        try
        {
            var orderId = await repo.CreateORder(storeId);
            return Ok(orderId);
        }
        catch (Exception ex)
        {
            return BadRequest (ex.Message);
        }
    }
}