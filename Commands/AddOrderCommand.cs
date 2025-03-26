using InventoryApp.Contexts;
using InventoryApp.Helpers;
using InventoryApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryApp.Commands;

public class AddOrderCommand : Command
{

  private readonly MariaDatabaseContext _mariaDatabaseContext;

  public override bool WaitForContinue => true;

  public override int Priority => 2;

  public AddOrderCommand() : base()
  {
    _mariaDatabaseContext = GlobalServiceProvider.GetInstance().GetRequiredService<MariaDatabaseContext>();
  }

  public override void Execute()
  {
    var orderNumber = PromptHelper.PromptInt("Order number");

    if (!orderNumber.HasValue) return;

    var order = new Order { Number = orderNumber.Value };

    _mariaDatabaseContext.Orders.Add(order);
    _mariaDatabaseContext.SaveChanges();

    LogHelper.Success($"Added a new order [{order.Number}]");
  }

  public override string GetName() => "Add an order";
}