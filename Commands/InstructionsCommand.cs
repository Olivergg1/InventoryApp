

using InventoryApp.Commands;
using InventoryApp.Helpers;

record Instruction(string Name, string Content)
{
  public override string ToString() => Name;
}

public class InstructionsCommand : Command
{
  public override bool WaitForContinue => true;

  public override int Priority => 1;

  public override string GetName() => "Instructions";

  public override void Execute()
  {
    var dummy = PromptHelper.Prompt("Namn");
    var instructions = new List<Instruction>([ new Instruction("Orders", "Add an order") ]);

    var instruction = PromptHelper.Search(instructions, (item, term) => item.Name.Contains(term, StringComparison.CurrentCultureIgnoreCase));

    if (instruction  == null) return;

    PrintInstructions(instruction);
  }

  private static void PrintInstructions(Instruction instruction)
  {
    Console.WriteLine($"=== {instruction.Name} ===");
    Console.WriteLine(instruction.Content);
  }
}