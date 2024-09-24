using api.Data.Models;

namespace api.Data.Strategies.FilterControllerStrategy.Strategies;

public class ScreenStrategy : IFilterControllerStrategy
{
    
    public Type type { get; } = typeof(Screen);
}