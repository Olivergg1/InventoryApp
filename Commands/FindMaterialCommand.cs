using InventoryApp.Contexts;
using InventoryApp.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class FindMaterialCommand : Command
{
  private readonly MariaDatabaseContext _mariaDatabaseContext;
  public override bool WaitForContinue => true;

  public override int Priority => 0;

  public FindMaterialCommand() : base()
  {
    _mariaDatabaseContext = GlobalServiceProvider.GetInstance().GetRequiredService<MariaDatabaseContext>();
  }

  public override void Execute()
  {
    var materials = _mariaDatabaseContext.Materials.ToList();

    // Let user select a material to remove
    var material = PromptHelper.Search(materials, 
      (item, term) => item.Title.Contains(term, StringComparison.CurrentCultureIgnoreCase), 
      new SearchOptions("Material"));

    if (material == null)
    {
      LogHelper.Error("No material selected");
      return;
    }

    Console.Out.WriteLine($"Id: {material.Id}");
    Console.Out.WriteLine($"Title: {material.Title}");
    Console.Out.WriteLine($"Orders: {material.Orders.Count} references");
  }

  public override string GetName() => "Find a material";
}