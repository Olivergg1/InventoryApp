
using System.Collections.Immutable;
using System.Reflection;

namespace InventoryApp.Commands;

public class CommandRegistry
{
  private static ImmutableList<CommandCollection> _commandCollections = [];
  private static ImmutableList<Command> _commands = [];

  public static void RegisterCommands() 
  {
    // Get all types that inherit from Command
    var commandTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && typeof(CommandCollection).IsAssignableFrom(t))
        .ToList();

    var commandCollections = commandTypes.Select(t => (CommandCollection)Activator.CreateInstance(t)!).ToImmutableList();
    _commandCollections = [.. commandCollections.OrderBy(c => c.GetCommandCollectionGroup())];

    _commands = [.. _commandCollections.SelectMany(cc => cc.GetCommands())];
  }

  public static bool ExecuteCommand(int index)
  {
    // Check if index is valid
    if (index < 0 || index > _commands.Count - 1) return false;

    // Execute command
    var Command = _commands[index];
    Command.SystemExecute();

    return true;
  }

  public static ImmutableList<CommandCollection> GetCommands() => _commandCollections;

  public static void ListCommands()
  {
    Console.Clear();
    Console.WriteLine("Available commands: ");

    foreach(var (command, i) in _commands.Select((command, index) => (command, index)))
    {
      Console.WriteLine($"{i + 1}: {command.GetName()}");
    }
  }
}