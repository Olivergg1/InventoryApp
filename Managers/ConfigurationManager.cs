

using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using InventoryApp.Helpers;

namespace InventoryApp.Managers;

public record AppConfiguration
{

  [JsonConverter(typeof(ProtectedStringConverter))]
  public string? ConntectionString { get; set; }

  [JsonConverter(typeof(ProtectedStringConverter))]
  public string? SecurityPassphrase { get; set; }
}

public class ConfigurationManager
{

  private const string ConfigFilePath = "Configuration.json";

  private readonly IDataProtector _protector;

  public ConfigurationManager(IDataProtectionProvider provider)
  {
    _protector = provider.CreateProtector("InventoryApp");

    ProtectedStringConverter.SetProtector(_protector);

    EnsureConfigFileExists();
  }

  public AppConfiguration GetConfiguration()
  {
    if (!File.Exists(ConfigFilePath)) {
      CreateConfigurationFile();

      return new AppConfiguration(); 
    }
       
    var json = File.ReadAllText(ConfigFilePath);
    var config = JsonConvert.DeserializeObject<AppConfiguration>(json) ?? new AppConfiguration();

    return config;
  }

  public void SaveConfig(AppConfiguration config)
  {
    var json = JsonConvert.SerializeObject(config, Formatting.Indented);
    File.WriteAllText(ConfigFilePath, json);
  }

  private void EnsureConfigFileExists()
  {
    if (!File.Exists(ConfigFilePath)) SaveConfig(new AppConfiguration()); // Create an empty config file
  }

  private void CreateConfigurationFile()
  {
    var config = new AppConfiguration();
    var json = JsonConvert.SerializeObject(config, Formatting.Indented);

    File.WriteAllText(ConfigFilePath, json);
  }

}