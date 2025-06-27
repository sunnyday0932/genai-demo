using GenAiDemo.Application.Dtos;
using GenAiDemo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenAiDemo.Interfaces.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var id = await _service.CreateOrderAsync(dto);
        return Ok(id);
    }
}
