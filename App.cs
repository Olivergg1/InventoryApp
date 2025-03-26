using InventoryApp.Commands;
using InventoryApp.Contexts;
using InventoryApp.Helpers;
using InventoryApp.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class App(MariaDatabaseContext context, ConnectionManager connectionManager, IServiceProvider serviceProvider)
{
  private IServiceProvider _serviceProvider = serviceProvider;
  private readonly MariaDatabaseContext _context = context;
  private readonly ConnectionManager _connectionManager = connectionManager;

  public void Start()
  {
    LogHelper.System("Application is starting...");

    var auth = GlobalServiceProvider.GetInstance().GetRequiredService<AuthenticationManager>();
    auth.Auth(clear: true);

    // Check if connection string is OK, otherwise update it
    LogHelper.System("Testing database connection...");

    while(!_context.Database.CanConnect())
    {
      Console.Clear();
      Console.WriteLine("Hoppsan! 😅 Något gick fel... Försök uppdatera din databas konfiguration!");

      var server = PromptHelper.Prompt("Server") ?? "localhost";
      var database = PromptHelper.Prompt("Database") ?? "database";
      var user = PromptHelper.Prompt("User") ?? "root";
      var password = PromptHelper.Pass("Password") ?? "password";

      _connectionManager.UpdateConnectionString(server, database, user, password);

      LogHelper.System("Updating your database configuration...");
      LogHelper.System("Testing database connection...");

      // Update database context connection string
      _context.Database.SetConnectionString(_connectionManager.GetConnectionString());
    }

    // Apply pending migrations
    _context.Database.Migrate();

    LogHelper.Success("Connection to database successful!");
    
    LogHelper.System("Loading data...");
    Thread.Sleep(2000);

    while (true)
    {
      Console.Clear();
      CommandRegistry.ListCommands();

      var index = PromptHelper.PromptInt("Choose an option");

      if (!index.HasValue) continue;

      CommandRegistry.ExecuteCommand(index.Value - 1);
    }
  }
}