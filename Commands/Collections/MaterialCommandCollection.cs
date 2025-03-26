namespace InventoryApp.Commands.Collections;

public class MaterialCommandCollection : CommandCollection
{
  private readonly List<Command> _commands;

  public override List<Command> GetCommands() => _commands;

  public MaterialCommandCollection() : base(CommandCollectionGroup.MATERIALS) 
  {
    _commands = [
      new AddMaterialCommand(),
      new FindMaterialCommand(),
      new RemoveMaterialCommand()
    ];
  }
}