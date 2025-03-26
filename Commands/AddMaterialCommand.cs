
using InventoryApp.Contexts;
using InventoryApp.Helpers;
using InventoryApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class AddMaterialCommand : Command
{
  private readonly MariaDatabaseContext _mariaDatabaseContext;
  public override bool WaitForContinue => true;

  public override int Priority => 0;

  public AddMaterialCommand() : base()
  {
    _mariaDatabaseContext = GlobalServiceProvider.GetInstance().GetRequiredService<MariaDatabaseContext>();
  }

  public override void Execute()
  {
    var title = PromptHelper.Prompt("Title");

    if (string.IsNullOrWhiteSpace(title)) {
      LogHelper.Error("Material title cannot be empty");
      return;
    }

    // Create new material with title
    var material = new Material { Title = title };

    // Save material to database
    _mariaDatabaseContext.Materials.Add(material);
    _mariaDatabaseContext.SaveChanges();

    LogHelper.Success($"Added material '{material.Title}'");
  }

  public override string GetName() => "Add a material";
}