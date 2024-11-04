using api.Data.Models;

namespace api.Data.Strategies.FilterControllerStrategy.Strategies;

public class PostStrategy(AppDbContext db) : IFilterControllerStrategy
{
    
    public Type type { get; } = typeof(Post);
    public IQueryable? GetDbObject(string where, string include)
    {
        var property = typeof(Screen).GetProperties().FirstOrDefault(x => x.Name == where);
        //db.Posts.Where(x => x.)
        
        return db.Posts.AsQueryable();
    }
}