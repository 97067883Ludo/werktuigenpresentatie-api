namespace api.Data.Strategies.FilterControllerStrategy;

public class FilterControllerStrategy : IFilterController
{
    private IEnumerable<IFilterControllerStrategy> _strategies { get; set; }

    public FilterControllerStrategy(IEnumerable<IFilterControllerStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IQueryable Execute(Type typeToStrategy, string where, string include)
    {
        return  _strategies.FirstOrDefault(x => x.type == typeToStrategy)?.GetDbObject(where, include) ?? throw new Exception();
    }
}

public interface IFilterController
{
    public IQueryable Execute(Type typeToStrategy, string where, string include);
}