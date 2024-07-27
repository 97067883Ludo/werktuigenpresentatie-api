using api.Data;
using api.Data.Dto.Category;
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
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public IActionResult GetOneItem(int id)
    {
        Category? category = _db.Categories.Find(id);

        if (category == null)
        {
            return NotFound("category not found");
        }
        
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> NewCategory(CategoryCreationDto? newCategory)
    {
        if (newCategory == null)
        {
            return BadRequest("fill in category propperties");
        }

        if (newCategory.Name == null)
        {
            return BadRequest("fill in a name");
        }

        Category category = new Category() { Name = newCategory.Name, CreationDate = DateTime.Now, UpdateDate = DateTime.Now};

        _db.Categories.Add(category);

        await _db.SaveChangesAsync();
        
        return Ok();
    }
    
}