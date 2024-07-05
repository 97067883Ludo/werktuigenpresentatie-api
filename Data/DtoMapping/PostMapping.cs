using api.Data.Dto.ResponseDto;
using api.Data.Models;

namespace api.Data.DtoMapping;

public static class PostMapping
{
    public static PostResponseDto MapResponseDto(Post post, string url)
    {
        if (post.Image == null)
        {
            return new PostResponseDto()
            {
                Id = post.Id,
                Name = post.Name,
                Url = post.Url,
                CreationDate = post.CreationDate,
                UpdateDate = post.UpdateDate,
                Image = new ImageResponseDto(),
            };   
        }
        
        return new PostResponseDto()
        {
            Id = post.Id,
            Name = post.Name,
            Url = post.Url,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate,
            Image = new ImageResponseDto()
            {
                Id = post.Image.Id,
                //TODO: ConvertImageToUrl nu hardcoded.
                url = url + "/image/?id=" + post.Image.Id
            }
        };
    }
    
    public static List<PostResponseDto> MapResponseDto(List<Post> post, string url)
    {
        List<PostResponseDto> listOfPosts = new (); 
        
        foreach (Post post1 in post)
        {
            listOfPosts.Add(MapResponseDto(post1, url));
        }

        return listOfPosts;
    }
}