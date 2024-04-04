using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }
}