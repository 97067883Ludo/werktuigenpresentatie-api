namespace api.Data.Dto.Category;

public class CategoryUpdateDto
{
    public int? Id { get; set; }

    public string? NewName { get; set; } = string.Empty;
}