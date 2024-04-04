using System.Data.Common;
using api.Data.Models;
using Microsoft.EntityFrameworkCore;

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
        string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        
        string? strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

        if (strWorkPath == null)
        {
            throw new Exception("could not obtain a working directory");
        }

        string connectionString = "Data Source=" + strWorkPath + _configuration.GetConnectionString("Default");
        
        optionsBuilder.UseSqlite(connectionString);
    }

    public DbSet<Post> Posts { get; set; }
}