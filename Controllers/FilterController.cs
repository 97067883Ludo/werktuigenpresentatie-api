using api.Data;
using api.Data.Models;
using api.Data.Strategies.FilterControllerStrategy;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[controller]")]
[EnableCors("Cors")]
public class FilterController : ControllerBase
{
    
    private readonly AppDbContext _db;
    
    private IFilterController _strategy;
    
    public FilterController(AppDbContext db, IFilterController filterController)
    {
        _db = db;
        _strategy = filterController;
    }

    [HttpGet]
    public IActionResult Get(string selector, string? where, string? include)
    {
        
        var test = _strategy.Execute(typeof(Screen));
        
        
        // if (_db.GetType().GetProperty(selector) == null)
        // {
        //     return BadRequest("Invalid Selector");
        // }
        //
        // var Dbropperty = _db.GetType().GetProperty(selector);
        //
        // if (Dbropperty == null)
        // {
        //     return BadRequest("Internal server error");
        // }
        //
        // var test = _db.GetType().GetProperty(selector)?.GetValue(selector, null);
        //
        return Ok();
    }
}