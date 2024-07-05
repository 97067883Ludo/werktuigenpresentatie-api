using System.Data.Common;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Directory = api.Data.Helpers.Directory;

namespace api.Data;

public class AppDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=" + Directory.GetWorkingDirectory() + _configuration.GetConnectionString("Default");
        
        optionsBuilder.UseSqlite(connectionString);
    }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Category> Categories { get; set; }
}