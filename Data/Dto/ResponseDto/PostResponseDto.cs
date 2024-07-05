using api.Data.Models;

namespace api.Data.Dto.ResponseDto;

public class PostResponseDto : CreatedUpdatedBase
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public ImageResponseDto Image { get; set; }
}