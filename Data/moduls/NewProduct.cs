using Microsoft.Identity.Client;

namespace Data.moduls;
public class NewProduct
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Location {get; set;}

    public int UserId{get;set;}


}