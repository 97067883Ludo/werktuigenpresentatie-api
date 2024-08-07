using api.Data;
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

        return PhysicalFile(image.ImagePath, "image/" + image.ImagePath.Split(".").Last());
    }
}