using api.Data;
using api.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("[Controller]")]
public class PostController : ControllerBase
{
    private AppDbContext Db { get; set; }

    public PostController(AppDbContext appDbContext)
    {
        Db = appDbContext;
    }

    [HttpGet]
    [EnableCors("Cors")]
    public ActionResult Get()
    {
        var test = Db.Posts.ToList();

        if (test == null) return BadRequest("no data");

        return Ok(test);
    }

    [HttpPost]
    public ActionResult PostData(PostDto? postDto)
    {
        if (postDto == null) return BadRequest("No post");

        if (postDto.Name == "") return BadRequest("No Post Name");
        
        if (postDto.Url == "") return BadRequest("No Post Name");

        Post post = new Post
        {
            Name = postDto.Name,
            Url = postDto.Url,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };
        
        Db.Posts.Add(post);
        Db.SaveChanges();
        
        return Ok(post);
    }

    [HttpPut]
    public ActionResult PutData(PostDto? postDto) 
    {
        if(postDto == null) return BadRequest("No data");

        if(postDto.id == null || postDto.id == 0) return BadRequest("No data");

        Post? post = Db.Posts.Find(postDto.id);

        if(post == null) return NotFound("No post found");

        post.Name = postDto.Name;

        post.Url = postDto.Url;

        post.UpdateDate = DateTime.Now;

        Db.SaveChanges();

        return Ok(post);
    }

    [HttpDelete]
    public ActionResult DeleteData(int? id) {
        
        if(id == null || id == 0) return BadRequest("No id filled");

        Post? post = Db.Posts.Find(id);

        if(post == null) return NotFound("no object with that id");

        Db.Posts.Remove(post);

        Db.SaveChanges();
        
        return Ok();

    }
}