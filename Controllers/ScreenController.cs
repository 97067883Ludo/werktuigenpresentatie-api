using api.Data;
using api.Data.Dto.Screen;
using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
        List<Screen> screens = _db.Screens.ToList();

        return Ok(screens);
    }

    [HttpGet("id")]
    public IActionResult Get(int? screenId)
    {
        if (screenId == null)
            return BadRequest("Screen cannot be null");
        
        Screen? screen = _db.Screens.Find(screenId);

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

        Screen? screen = _db.Screens.Find(screenUpdateDto.screenId);

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
}