using Microsoft.EntityFrameworkCore;
using InventoryApp.Managers;
using Microsoft.EntityFrameworkCore.Design;

/*
public class DesignTimeMariaDatabaseContextFactory : IDesignTimeDbContextFactory<MariaDatabaseContext>
{
  private readonly MySqlServerVersion DB_SERVER_VERSION = new(new Version(8, 0, 23));

  public MariaDatabaseContext CreateDbContext(string[] args)
  {
    
    var options = new DbContextOptionsBuilder<MariaDatabaseContext>()
      .UseMySql("server=localhost;database=InventoryDB;user=oliver;password=Banankartong2025!;", DB_SERVER_VERSION)
      .Options;

    return new MariaDatabaseContext(options);
  }
}

public class MariaDatabaseContextFactory
{
  private readonly IServiceProvider _serviceProvider;
  private readonly ConnectionManager _connectionManager;

  private readonly MySqlServerVersion DB_SERVER_VERSION = new(new Version(8, 0, 23));

  public MariaDatabaseContextFactory(IServiceProvider serviceProvider, ConnectionManager connectionManager)
  {
    _serviceProvider = serviceProvider;
    _connectionManager = connectionManager;

    _connectionManager.OnConnectionStringChanged += RebuildContext;
  }

  private MariaDatabaseContext? _context;

  public MariaDatabaseContext GetContext()
  {
    _context ??= CreateNewContext();

    return _context;
  }

  private MariaDatabaseContext CreateNewContext()
  {
    var options = new DbContextOptionsBuilder<MariaDatabaseContext>()
      .UseMySql(_connectionManager?.GetConnectionString(), DB_SERVER_VERSION)
      .Options;

    return new MariaDatabaseContext(options);
  }

  private void RebuildContext()
  {
    _context?.Dispose();
    _context = CreateNewContext();
  }
}
*/