using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers;

[ApiController]
[Route("api/[controller]")]


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class MasterDataController : ControllerBase
{
    private static List<Product> _products = new();
    private readonly ILogger<MasterDataController> _logger;

    // Конструктор для DI
    public MasterDataController(ILogger<MasterDataController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogInformation("GET all products");
        return Ok(_products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}