using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;

[ApiController]
[Route("api/categories")]
public class CategoriesApiController : ControllerBase
{
    private readonly AppDbContext _db;
    public CategoriesApiController(AppDbContext db) => _db = db;

    // GET: /api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        => Ok(await _db.Categories.AsNoTracking()
                .Select(c => new CategoryDto(c.Id, c.Name))
                .ToListAsync());

    public record CategoryDto(int Id, string Name);
}
