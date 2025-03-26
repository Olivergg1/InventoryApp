using InventoryApp.Managers;
using InventoryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Contexts;

public class MariaDatabaseContext(IServiceProvider serviceProvider) : DbContext()
{
  private readonly MySqlServerVersion DB_SERVER_VERSION = new(new Version(8, 4, 4));

  public DbSet<Order> Orders { get; set; }
  public DbSet<Material> Materials { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    var connectionString = "server=localhost;database=InventoryDB;user=oliver;password=Banankartong2025!;";
    
    try {
      var connectionManager = serviceProvider.GetRequiredService<ConnectionManager>();
      connectionString = connectionManager.GetConnectionString();
    }
    catch
    {}

    optionsBuilder.UseMySql(connectionString, DB_SERVER_VERSION);
  }
}