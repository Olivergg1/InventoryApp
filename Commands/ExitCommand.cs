using InventoryApp.Helpers;

namespace InventoryApp.Commands;

public class ExitCommand : Command
{
  public override bool WaitForContinue => false;

  public override int Priority => 0;

  public override void Execute()
  {
    LogHelper.System("Terminating application...");
    Thread.Sleep(1000);
    Environment.Exit(0);
  }

  public override string GetName() => "Exit";
}