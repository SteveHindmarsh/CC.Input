using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CC.Input.Data;

public class InputDbContext : DbContext
{
    public InputDbContext(DbContextOptions<InputDbContext> options) : base(options)
    {

    }
    public DbSet<Logic.Model.Input> Inputs { get; set; }
}

///// <summary>
///// Helper to allow Migration classes to be stored in the project which decalares the DbContext when Add-Migration [name] is ran.
///// https://khalidabuhakmeh.com/fix-unable-to-resolve-dbcontextoptions-for-ef-core
///// However; must be removed when running Update-Database - can't win!
///// </summary>
//public class DatabaseDesignTimeDbContextFactory
//: IDesignTimeDbContextFactory<InputDbContext>
//{
//    public InputDbContext CreateDbContext(string[] args)
//    {
//        var builder = new DbContextOptionsBuilder<InputDbContext>();
//        builder.UseSqlServer();
//        return new InputDbContext(builder.Options);
//    }
//}
