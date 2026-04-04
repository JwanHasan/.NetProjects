using System.ComponentModel;

namespace Data.moduls;
public class User
{
    public int Id {get;set;}
    public required string UserName {get;set;}
    public required string Password {get;set;}
    public bool IsAdmin {get;set;} = false;


    // each user can create multiple products but only product is created by one user
   public  ICollection<Product> Products {get;set;} = new List<Product>();

}