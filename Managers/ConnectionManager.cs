using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;

namespace InventoryApp.Managers;

public class ConnectionManager
{
  private string _connectionString;
  private readonly ConfigurationManager _configManager;

  private AppConfiguration _config => _configManager.GetConfiguration();

  // Event triggered when connection string is updated
  public event Action? OnConnectionStringChanged;

  public ConnectionManager(ConfigurationManager configurationManager)
  {
    _configManager = configurationManager;
    _connectionString = LoadConnectionString();
  }

  public string GetConnectionString() => _configManager.GetConfiguration().ConntectionString ?? GetDefaultConnectionString();

  public void UpdateConnectionString(string host, string database, string user, string password)
  {
    var connectionString = $"server={host};database={database};user={user};password={password}";
    _connectionString = connectionString;
    SaveConnectionString(connectionString);

    // Notify context factory that the connection string was updated
    OnConnectionStringChanged?.Invoke();
  }

  private void SaveConnectionString(string connectionString)
  {
    var config = _config;
    config.ConntectionString = connectionString;
    
    _configManager.SaveConfig(config);
  }

  private string LoadConnectionString()
  {
    return _connectionString;
  }

  private string GetDefaultConnectionString()
  {
    return "server=localhost;database=database;user=user;password=password";
  }
}