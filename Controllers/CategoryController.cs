using api.Data;
using api.Data.Dto.Category;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        List<Category> categories = _db.Categories.Include(x => x.Posts).ToList();
        
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
        
        return Ok(category);
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
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]    
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
            return NotFound("no category found");
        }

        if (!string.IsNullOrEmpty(newCategory.NewName) || !string.IsNullOrWhiteSpace(newCategory.NewName))
        {
            categoryToBeUpdated.Name = newCategory.NewName;
            categoryToBeUpdated.UpdateDate = DateTime.Now;
        }

        _db.SaveChanges();
        
        return Ok(categoryToBeUpdated);
    }


    [HttpPost("AddPost")]
    public IActionResult AddPost(int? PostId, int? CategoryId)
    {
        Post? post = _db.Posts.Find(PostId);
        
        if (post == null)
        {
            return BadRequest("post not found");
        }
        
        Category? category = _db.Categories.Find(CategoryId);

        if (category == null)
        {
            return BadRequest("No category found with that id");
        }
        
        category.Posts.Add(post);
        
        _db.SaveChanges();
        
        return Ok(category);
    }
}