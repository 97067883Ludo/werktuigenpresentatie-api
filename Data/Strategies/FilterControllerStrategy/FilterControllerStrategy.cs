namespace api.Data.Strategies.FilterControllerStrategy;

public class FilterControllerStrategy : IFilterController
{
    private IEnumerable<IFilterControllerStrategy> _strategies { get; set; }

    public FilterControllerStrategy(IEnumerable<IFilterControllerStrategy> strategies)
    {
        _strategies = strategies;
    }

    public Type Execute(Type typeToStrategy)
    {
        Type? test = _strategies.Where(x => x.type == typeToStrategy).FirstOrDefault()?.type;
        return test;
    }

}

public interface IFilterController
{
    public Type Execute(Type typeToStrategy);
}