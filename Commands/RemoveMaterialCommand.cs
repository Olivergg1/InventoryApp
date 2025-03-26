
using InventoryApp.Contexts;
using InventoryApp.Helpers;
using InventoryApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class RemoveMaterialCommand : Command
{
  private readonly MariaDatabaseContext _mariaDatabaseContext;
  private readonly AuthenticationManager _authManager;

  public override bool WaitForContinue => true;

  public override int Priority => 0;

  public RemoveMaterialCommand() : base()
  {
    _mariaDatabaseContext = GlobalServiceProvider.GetInstance().GetRequiredService<MariaDatabaseContext>();
    _authManager = GlobalServiceProvider.GetInstance().GetRequiredService<AuthenticationManager>();
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

    // Authorize user
    _authManager.Auth();

    // Save material to database
    _mariaDatabaseContext.Materials.Remove(material);
    _mariaDatabaseContext.SaveChanges();

    LogHelper.Success($"Removed material [{material.Id}] with title '{material.Title}'");
  }

  public override string GetName() => "Remove a material";
}