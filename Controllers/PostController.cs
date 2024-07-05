using api.Data;
using api.Data.DtoMapping;
using api.Data.Helpers;
using api.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    
    [HttpGet("id")] 
    public ActionResult GetId(int id)
    {
        Post? post = Db.Posts.Include(x => x.Image).FirstOrDefault(x => x.Id == id);
    
        if (post == null)
        {
            return NotFound();
        }
        
        return Ok(PostMapping.MapResponseDto(post, _configuration["BaseUrl"] ?? ""));
    }

    #region Post psot
     [HttpPost] 
        public async Task<ActionResult> PostData(PostPostDto? postDto)
        {
            if (postDto == null) return BadRequest("No post");
    
            if (string.IsNullOrEmpty(postDto.Name)) return BadRequest("No Post Name");
            
            if (string.IsNullOrEmpty(postDto.Url)) return BadRequest("No Post URL");
    
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
            await Db.SaveChangesAsync();
            
            return Ok(post);
        }
    #endregion

    #region Put Post
    [HttpPut]
    public async Task<ActionResult> PutData(PutPostDto? postDto) 
    {
        if(postDto == null) return BadRequest("No data");

        if(postDto.id == 0) return BadRequest("No data");

        Post? post = Db.Posts.Where(x => x.Id == postDto.id).Include(post => post.Image).FirstOrDefault();
        
        if(post == null) return NotFound("No post found");

        if (!string.IsNullOrEmpty(postDto.Name)) post.Name = postDto.Name;

        if (!string.IsNullOrEmpty(postDto.Url)) post.Url = postDto.Url;

        if (postDto.FormFile?.Length > 0)
        {
            if (post.Image == null)
            {
                Image newImage = new Image() { ImagePath = await ImageStore.StoreImage(postDto.FormFile) };
                
                post.Image = newImage;
                
                Db.Images.Add(newImage);
            }
            else
            {
                post.Image.ImagePath = await ImageStore.ReplaceImage(post.Image, postDto.FormFile);
            }
        }
        
        post.UpdateDate = DateTime.Now;

        await Db.SaveChangesAsync();

        return Ok(post);
    }    
    #endregion

    #region Delete Post
    [HttpDelete]
    public ActionResult DeleteData(int? id) 
    {
        if(id == null || id == 0) return BadRequest("No id filled");
        
        Post? post = Db.Posts.Include(x => x.Image).FirstOrDefault(x => x.Id == id);

        if(post == null) return NotFound("no object with that id");
        
        if (post.Image != null)
        {
            Db.Images.Remove(post.Image);
        }
        
        Db.Posts.Remove(post);

        Db.SaveChanges();
        
        return Ok();
    }    
    #endregion
}