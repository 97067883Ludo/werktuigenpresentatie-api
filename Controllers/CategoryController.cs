using api.Data;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[Controller]")]
public class CategoryController : ControllerBase
{

    private readonly AppDbContext _db;
    
    public CategoryController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public IActionResult GetAll()
    {
        List<Category> categories = _db.Categories.ToList();
        
        return Ok(categories);
    }

    [HttpGet("id")]
    public IActionResult GetOneItem(int id)
    {
        Category? category = _db.Categories.Find(id);

        if (category == null)
        {
            return NotFound("category not found");
        }
        
        return Ok(category);
    }
}