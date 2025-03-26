using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApp.Models;

[Table("orders")]
public class Order
{
  [Key]
  public int Id {get; set; }

  [Required]
  public int Number { get; set; }

  public List<Material> Materials { get; set; } = [];

  public override string ToString() => Number.ToString();
}