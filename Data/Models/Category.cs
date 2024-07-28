using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Category : ICreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = String.Empty;

    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}