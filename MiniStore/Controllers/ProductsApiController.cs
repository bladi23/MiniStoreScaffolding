using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniStore.Data;          
using MiniStore.Models;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsApiController(AppDbContext db) => _db = db;

    // GET: /api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductListDto>>> Get()
    {
        var data = await _db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Select(p => new ProductListDto {
                Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock,
                CategoryId = p.CategoryId, CategoryName = p.Category!.Name
            })
            .ToListAsync();

        return Ok(data);
    }

    // POST: /api/products
    [HttpPost]
    public async Task<ActionResult<ProductListDto>> Post(ProductCreateDto dto)
    {
        if (!await _db.Categories.AnyAsync(c => c.Id == dto.CategoryId))
            return BadRequest("CategoryId not found.");

        var entity = new Product {
            Name = dto.Name.Trim(),
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId
        };

        _db.Products.Add(entity);
        await _db.SaveChangesAsync();

        var created = await _db.Products
            .Include(p => p.Category)
            .Where(p => p.Id == entity.Id)
            .Select(p => new ProductListDto {
                Id = p.Id, Name = p.Name, Price = p.Price, Stock = p.Stock,
                CategoryId = p.CategoryId, CategoryName = p.Category!.Name
            }).FirstAsync();

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    public record ProductCreateDto(string Name, decimal Price, int Stock, int CategoryId);
    public record ProductListDto {
        public int Id { get; init; }
        public string Name { get; init; } = "";
        public decimal Price { get; init; }
        public int Stock { get; init; }
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = "";
    }
}
