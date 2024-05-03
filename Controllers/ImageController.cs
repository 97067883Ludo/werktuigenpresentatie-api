using api.Data;
using api.Data.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[Controller]")]
public class ImageController : ControllerBase
{
    private AppDbContext Db { get; set; }

    public ImageController(AppDbContext appDbContext)
    {
        Db = appDbContext;
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        var image = Db.Images.Find(id);

        if (image == null)
        {
            return NotFound();
        }
        
        //TODO: image/webp is nu hardcoded
        return PhysicalFile(image.ImagePath, "image/webp");
    }
}