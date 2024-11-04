using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Data.Models;

public class Category : ICreatedUpdatedBase
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = String.Empty;
    
    [ForeignKey("Screen")]
    public int? ScreenId { get; set; }
    
    //relations
    public List<Post> Posts { get; set; } = new List<Post>();

    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}