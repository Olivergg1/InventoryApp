namespace InventoryApp.Commands.Collections;

public class SystemCommandCollection : CommandCollection
{
  private readonly List<Command> _commands;

  public override List<Command> GetCommands() => _commands;

  public SystemCommandCollection() : base(CommandCollectionGroup.SYSTEM) 
  {
    _commands = [
      // TODO: Add InstructionsCommand when it's fully implemented
      // TODO: Add ChangeDatabaseCommand when it's implemented
      new SetPassphraseCommand(),
      new ExitCommand()
    ];
  }
}