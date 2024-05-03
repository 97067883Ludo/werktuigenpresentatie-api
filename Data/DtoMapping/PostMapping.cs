using api.Data.Dto.ResponseDto;
using api.Data.Helpers;
using api.Data.Models;

namespace api.Data.DtoMapping;

public static class PostMapping
{
    public static PostResponseDto MapResponseDto(Post post)
    {
        if (post.Image == null)
        {
            return new PostResponseDto()
            {
                Id = post.Id,
                Name = post.Name,
                Url = post.Url,
            };   
        }
        
        return new PostResponseDto()
        {
            Id = post.Id,
            Name = post.Name,
            Url = post.Url,
            image = new ImageResponseDto()
            {
                Id = post.Image.Id,
                //TODO: ConvertImageToUrl nu hardcoded.
                url = "localhost:5172/image?id=" + post.Image.Id
            }
        };
    }
    
    public static List<PostResponseDto> MapResponseDto(List<Post> post)
    {
        List<PostResponseDto> listOfPosts = new (); 
        
        foreach (Post post1 in post)
        {
            listOfPosts.Add(MapResponseDto(post1));
        }

        return listOfPosts;
    }
}