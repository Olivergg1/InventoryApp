
using System.Collections.Immutable;
using System.Reflection;

namespace InventoryApp.Commands;

public class CommandRegistry
{
  private static ImmutableList<Command> Commands = [];

  public static void RegisterCommands() 
  {
    var commandList = new List<Command>();

    // Get all types that inherit from Command
    var commandTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && typeof(Command).IsAssignableFrom(t))
        .ToList();

    Commands = commandTypes.Select(t => (Command)Activator.CreateInstance(t)).OrderByDescending(c => c.Priority).ToImmutableList();
  }

  public static bool ExecuteCommand(int index)
  {
    // Check if index is valid
    if (index < 0 || index > Commands.Count - 1) return false;

    // Execute command
    var Command = Commands[index];
    Command.SystemExecute();

    return true;
  }

  public static ImmutableList<Command> GetCommands() => Commands;

  public static void ListCommands()
  {
    Console.Clear();
    Console.WriteLine("Available commands: ");

    for(int i = 0; i < Commands.Count; i++)
    {
      var command = Commands[i];

      Console.WriteLine($"{i + 1}: {command.GetName()}");
    }
  }

}