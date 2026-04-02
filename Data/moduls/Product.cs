using System.ComponentModel.DataAnnotations.Schema;

namespace Data.moduls;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Location {get; set;}
  
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime ArrivedDate {get;set;}

}