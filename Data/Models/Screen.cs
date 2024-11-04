using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Screen : ICreatedUpdatedBase
{
    [Key] 
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    public bool Online { get; set; } = false;

    public DateTime LastSeenOnline { get; set; }

    //relations
    public List<Post>? Posts { get; set; } = new();
    
    public List<Category>? Categories { get; set; } = new();
    
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}