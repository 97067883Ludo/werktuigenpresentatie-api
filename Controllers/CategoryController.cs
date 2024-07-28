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

    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult DeleteCategory(int? id)
    {
        if (id == null)
        {
            return BadRequest("no id has been filled in.");
        }

        Category? categoryToDelete = _db.Categories.Find(id);

        if (categoryToDelete == null)
        {
            return BadRequest("no category found with that id.");
        }

        _db.Categories.Remove(categoryToDelete);

        _db.SaveChanges();
        
        return Ok("category deleted");
    }

    [HttpPut]
    public IActionResult UpdateCategory(CategoryUpdateDto? newCategory)
    {
        if (newCategory == null)
        {
            return BadRequest("You need to give a category");
        }

        if (newCategory.Id == null)
        {
            return BadRequest("give an category id");
        }

        Category? categoryToBeUpdated = _db.Categories.Find(newCategory.Id);

        if (categoryToBeUpdated == null)
        {
            return BadRequest("no category found");
        }

        if (!string.IsNullOrEmpty(newCategory.NewName) || !string.IsNullOrWhiteSpace(newCategory.NewName))
        {
            categoryToBeUpdated.Name = newCategory.NewName;
            categoryToBeUpdated.UpdateDate = DateTime.Now;
        }

        _db.SaveChanges();
        
        return Ok(categoryToBeUpdated);
    }
}