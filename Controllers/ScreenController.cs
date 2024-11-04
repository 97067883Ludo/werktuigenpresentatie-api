using api.Data;
using api.Data.Dto.Screen;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("[Controller]")]
public class ScreenController : ControllerBase
{

    private readonly AppDbContext _db;

    public ScreenController(AppDbContext database)
    {
        _db = database;
    }

    [HttpGet]
    [ProducesResponseType(200)] 
    public IActionResult GetAll()
    {
        List<Screen> screens = _db.Screens.Include(x => x.Posts)!.ThenInclude(x => x.Image)
            .Include(x => x.Categories)!.ThenInclude(x => x.Posts)
            .ToList();

        return Ok(screens);
    }

    [HttpGet("id")]
    public IActionResult Get(int? screenId)
    {
        if (screenId == null)
            return BadRequest("Screen cannot be null");
        
        Screen? screen = _db.Screens.Include(x => x.Posts)!.ThenInclude(x => x.Image)
            .Include(x => x.Categories)!.ThenInclude(x => x.Posts).FirstOrDefault(x => x.Id == screenId);

        if (screen == null)
            return NotFound("Could not find screen with that id");

        return Ok(screen);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Post(ScreenPostDto? newScreen)
    {
        if (newScreen == null)
            return BadRequest("Screen cannot be null");

        if (string.IsNullOrEmpty(newScreen.Name))
            return BadRequest("screen name cannot be empty");

        Screen addingScreen = new Screen()
        {
            Name = newScreen.Name,
            UpdateDate = DateTime.Now,
            CreationDate = DateTime.Now
        };

        _db.Screens.Add(addingScreen);

        _db.SaveChanges();

        return Ok(addingScreen);
    }
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult UpdateScreen(ScreenUpdateDto? screenUpdateDto)
    {
        if (screenUpdateDto == null)
            return BadRequest("Update Screen cannot be null");

        if (string.IsNullOrEmpty(screenUpdateDto.Name))
            return BadRequest("Screen name cannot be empty");

        Screen? screen = _db.Screens.Find(screenUpdateDto.ScreenId);

        if (screen == null)
            return NotFound("Could not find a screen");

        screen.Name = screenUpdateDto.Name;

        _db.SaveChanges();

        return Ok(screen);
    }

    [HttpDelete]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult DeleteScreen(int screenId = 0)
    {

        Screen? screen = _db.Screens.Find(screenId);

        if (screen == null)
            return BadRequest("Could not find screen");

        _db.Remove(screen);

        _db.SaveChanges();

        return Ok("done");
    }
    
    //relations

    [HttpPost("/Screen/AddCategory")]
    public IActionResult AddCategory(int ScreenId, int CategoryId)
    {
        Category? category = _db.Categories.Find(CategoryId);

        if (category == null)
        {
            return BadRequest("No category found");
        }

        Screen? screen = _db.Screens.Find(ScreenId);
        
        if (screen == null)
        {
            return BadRequest("No screen found");
        }
        
        screen.Categories?.Add(category);

        _db.SaveChanges();
        
        return Ok(screen);

    }

    [HttpPost("/Screen/AddPost")]
    public IActionResult AddPost(int ScreenId, int PostId)
    {
        Post? post = _db.Posts.Find(PostId);

        if (post == null)
        {
            return BadRequest("No post found");
        }
        
        Screen? screen = _db.Screens.Find(ScreenId);

        if (screen == null)
        {
            return BadRequest("No Screen found");
        }
        
        screen.Posts?.Add(post);
        
        _db.SaveChanges();
        
        return Ok(screen);
    }

    [HttpDelete("/Screen/RemoveCategory")]
    public IActionResult RemoveCategory(int ScreenId, int CategoryId)
    {
        Category? category = _db.Categories.Find(CategoryId);
        
        if (category == null)
        {
            return BadRequest("No category found");
        }
        
        Screen? screen = _db.Screens.Find(ScreenId);

        if (screen == null)
        {
            return BadRequest("No screen found");
        }

        category.ScreenId = null;

        _db.SaveChanges();
        
        return Ok(screen);
    }

    [HttpDelete("/Screen/RemovePost")]
    public IActionResult RemovePost(int ScreenId, int PostId)
    {
        Screen? screen = _db.Screens.Find(ScreenId);

        if (screen == null)
        {
            return BadRequest("No screen found");
        }

        Post? post = _db.Posts.Find(PostId);

        if (post == null)
        {
            return BadRequest("No post found");
        }
        
        screen.Posts?.Remove(post);
        
        _db.SaveChanges();
        
        return Ok(screen);
    }

}