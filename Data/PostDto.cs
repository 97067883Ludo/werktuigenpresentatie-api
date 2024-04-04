namespace api.Data;

public class PostDto
{
    public int? id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string Url { get; set; } = string.Empty;
}