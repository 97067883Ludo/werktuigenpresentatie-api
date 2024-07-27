using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Data.Models;

public class Post : ICreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
    
    public Image? Image { get; set; }

    public Category? Category { get; set; }
    
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}