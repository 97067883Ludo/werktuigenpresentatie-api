using System.ComponentModel.DataAnnotations;

namespace api.Data.Models;

public class Image : CreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }

    public string ImagePath { get; set; } = string.Empty;
}