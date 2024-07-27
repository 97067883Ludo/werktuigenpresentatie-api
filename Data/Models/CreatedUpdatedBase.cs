namespace api.Data.Models;

public interface ICreatedUpdatedBase
{
    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }
}