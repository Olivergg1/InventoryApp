namespace InventoryApp.Commands.Collections;

public class OrderCommandCollection : CommandCollection
{
  private readonly List<Command> _commands;

  public override List<Command> GetCommands() => _commands;

  public OrderCommandCollection() : base(CommandCollectionGroup.ORDERS) 
  {
    _commands = [
      new AddOrderCommand(),
      new FindOrderCommand(),
      // TODO: Add RemoveOrderCommand
    ];
  }
}