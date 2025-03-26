
namespace InventoryApp.Helpers;

public static class LogHelper
{
  public static void System(string? value)
  {
    Console.WriteLine($"\u001b[34m[System]\u001b[0m {value}");
  }

  public static void Error(string? value)
  {
    Console.WriteLine($"\u001b[31m[Error]\u001b[0m {value}");
  }

  public static void Success(string? value)
  {
    Console.WriteLine($"\u001b[32m[Success]\u001b[0m {value}");
  }
}