using api.Data;
using api.Data.DtoMapping;
using api.Data.Helpers;
using api.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace api.Controllers;

[Route("[Controller]")]
[EnableCors("Cors")]
public class PostController : ControllerBase
{
    private AppDbContext Db { get; set; }
    private IConfiguration _configuration { get; set; }

    public PostController(AppDbContext appDbContext, IConfiguration configuration)
    {
        Db = appDbContext;
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var posts = PostMapping.MapResponseDto(Db.Posts.Include(x => x.Image).ToList(), _configuration["BaseUrl"] ?? "");

        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult> PostData(PostPostDto? postDto)
    {
        if (postDto == null) return BadRequest("No post");

        if (string.IsNullOrEmpty(postDto.Name)) return BadRequest("No Post Name");
        
        if (string.IsNullOrEmpty(postDto.Url)) return BadRequest("No Post Name");

        Post post = new Post
        {
            Name = postDto.Name,
            Url = postDto.Url,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };
        
        //when image is provided store it and add it to the post
        if (postDto.FormFile != null && postDto.FormFile.ContentType.Split("/")[0] == "image")
        {
           string filePath = await ImageStore.StoreImage(postDto.FormFile);

           Image image = new Image()
           {
               ImagePath = filePath,
               CreationDate = DateTime.Now,
               UpdateDate = DateTime.Now
           };

           Db.Images.Add(image);

           post.Image = image;
        }
        
        Db.Posts.Add(post);
        Db.SaveChanges();
        
        return Ok(post);
    }

    [HttpPut]
    public ActionResult PutData(PutPostDto? postDto) 
    {
        if(postDto == null) return BadRequest("No data");

        if(postDto.id == 0) return BadRequest("No data");

        Post? post = Db.Posts.Include("image").Where(x => x.Id == postDto.id).FirstOrDefault();

        
        
        if(post == null) return NotFound("No post found");

        if (!string.IsNullOrEmpty(postDto.Name)) post.Name = postDto.Name;

        if (!string.IsNullOrEmpty(postDto.Url)) post.Url = postDto.Url;

        post.UpdateDate = DateTime.Now;

        Db.SaveChanges();

        return Ok(post);
    }

    [HttpDelete]
    public ActionResult DeleteData(int? id) 
    {
        if(id == null || id == 0) return BadRequest("No id filled");

        Post? post = Db.Posts.Find(id);

        if(post == null) return NotFound("no object with that id");

        Db.Posts.Remove(post);

        Db.SaveChanges();
        
        return Ok();
    }
}