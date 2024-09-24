using api.Data.Models;

namespace api.Data.Strategies.FilterControllerStrategy.Strategies;

public class PostStrategy : IFilterControllerStrategy
{
    
    public Type type { get; } = typeof(Post);
}