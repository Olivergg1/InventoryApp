
namespace InventoryApp.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class CommandAttribute(string name, int priority = 0) : Attribute
{
  public string CommandName { get; } = name;
  public int Priority = priority;
}