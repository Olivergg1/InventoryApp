using InventoryApp.Helpers;
using InventoryApp.Managers;

class AuthenticationManager(ConfigurationManager configManager)
{
  private readonly ConfigurationManager _configManager = configManager;

  private AppConfiguration Config => _configManager.GetConfiguration();

  public void Auth(bool clear = false)
  {
    EnsurePassphraseCreated();

    var pass = string.Empty;
    var attempts = 0;

    while (pass != Config.SecurityPassphrase)
    {
      // Clear console only if clear flag set to true
      if (clear) Console.Clear();

      if (attempts != 0) LogHelper.Error("You entered the wrong passphrase");

      pass = PromptHelper.Pass("Auth") ?? string.Empty;

      attempts++;
    }

    // Clear console when auth is successful
    Console.Clear();
  }

  public void EnsurePassphraseCreated()
  {
    if (Config.SecurityPassphrase != null) return;
    
    // Security passphrase has not been created
    Console.WriteLine("Saving default auth passphrase");
    
    var config = Config;
    config.SecurityPassphrase = "password";

    _configManager.SaveConfig(config);
  }
}