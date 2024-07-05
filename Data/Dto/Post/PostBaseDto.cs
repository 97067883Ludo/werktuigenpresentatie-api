namespace api.Data;

public abstract class PostBaseDto
{
    public string? Name { get; set; } = string.Empty;
    
    public string? Url { get; set; } = string.Empty;

    public IFormFile? FormFile { get; set; }
}