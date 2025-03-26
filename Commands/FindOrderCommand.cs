using InventoryApp.Contexts;
using InventoryApp.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class FindOrderCommand : Command
{
  private readonly MariaDatabaseContext _mariaDatabaseContext;
  
  public override bool WaitForContinue => true;

  public override int Priority => 2;

  public FindOrderCommand()
  {
    _mariaDatabaseContext = GlobalServiceProvider.GetInstance().GetRequiredService<MariaDatabaseContext>();
  }

  public override void Execute()
  {
    var orders = _mariaDatabaseContext.Orders.Include(order => order.Materials).ToList();
    var order = PromptHelper.Search(orders, (order, term) => order.Number.ToString().Contains(term), new SearchOptions("Order"));
    
    if (order == null) {
      LogHelper.Error("No order found");
      return;
    }

    LogHelper.Success($"Order [{order.Number}] has {order.Materials.Count} materials");
  }

  public override string GetName() => "Find an order";
}