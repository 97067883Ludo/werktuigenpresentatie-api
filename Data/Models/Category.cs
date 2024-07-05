using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Category : CreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = String.Empty;
}