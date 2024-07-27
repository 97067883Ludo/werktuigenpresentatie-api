using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Image : ICreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }

    public string ImagePath { get; set; } = string.Empty;
    
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}