namespace api.Data.Strategies.FilterControllerStrategy;

public interface IFilterControllerStrategy
{
    public Type type { get; }

    public IQueryable? GetDbObject(string where, string include);
}