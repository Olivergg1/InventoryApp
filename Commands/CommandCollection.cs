namespace InventoryApp.Commands;

public enum CommandCollectionGroup
{
  ORDERS,
  MATERIALS,
  SYSTEM,
  UNDEFINED = 9999, // Should always be last
}


/*** class CommandCollection 

  Collection of Commands that are related. 
  
  The order in which commands will be printed is directly related to a collection's
  CommandCollectionGroup (UNDEFINED will always be last)

***/
public abstract class CommandCollection(CommandCollectionGroup commandGroup = CommandCollectionGroup.UNDEFINED)
{
  public CommandCollectionGroup GetCommandCollectionGroup() => commandGroup;

  public abstract List<Command> GetCommands();
}