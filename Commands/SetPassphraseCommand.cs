using InventoryApp.Helpers;
using InventoryApp.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class SetPassphraseCommand : Command
{
  public override bool WaitForContinue => true;

  public override int Priority => 1;

  private static bool ComparePassphrase(string? p1, string? p2) => !string.IsNullOrEmpty(p1) && !string.IsNullOrEmpty(p1) && p1 == p2;

  public override void Execute()
  {
    var first = string.Empty;
    var confirmation = string.Empty;

    while(!ComparePassphrase(first, confirmation))
    {
      first = PromptHelper.Pass("New passphrase");
      confirmation = PromptHelper.Pass("Re-enter passphrase");
    }

    // Update passphrase in config
    var manager = GlobalServiceProvider.GetInstance().GetRequiredService<ConfigurationManager>();
    var config = manager.GetConfiguration();
    config.SecurityPassphrase = confirmation;

    manager.SaveConfig(config);

    LogHelper.Success("Passphrase saved");
  }

  public override string GetName() => "Change passphrase";
}