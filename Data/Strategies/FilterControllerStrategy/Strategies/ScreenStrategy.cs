using api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Strategies.FilterControllerStrategy.Strategies;

public class ScreenStrategy(AppDbContext db) : IFilterControllerStrategy
{
    
    public Type type { get; } = typeof(DbSet<Screen>);
    public IQueryable GetDbObject(string where, string include)
    {
        var property = typeof(Screen).GetProperties().FirstOrDefault(x => x.Name == where);

        IQueryable queryable;
        
        if (string.IsNullOrWhiteSpace(where))
        {
            queryable = db.Screens.FromSqlRaw("SELECT * from Screens");
            return queryable;
        }
        else
        {
            Screen test = new Screen();
            test.GetType().GetProperties();
            queryable = db.Screens.FromSqlRaw("SELECT * from Screens WHERE ");
        }
        
        return db.Screens.AsQueryable();
    }
}