
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApp.Models;

[Table("materials")]
public class Material
{
  [Key]
  public int Id {get; set; }

  [Required]
  public string Title { get; set; } = string.Empty;

  public List<Order> Orders { get; set; } = [];
}