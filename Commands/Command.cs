namespace InventoryApp.Commands;

public abstract class Command
{
  public abstract bool WaitForContinue { get; }

  public abstract int Priority { get; }

  public abstract string GetName();

  public abstract void Execute();

  public void SystemExecute()
  {
    Console.Clear();

    Execute(); // Execute the actual command logic

    if (WaitForContinue)
    {
      Console.WriteLine();
      Console.Write("Press enter to continue");
      Console.ReadKey();
      Console.Clear();
    }
  }
}