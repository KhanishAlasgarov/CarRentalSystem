using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistance.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Fuel> Fules { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }



    public BaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
