using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Identity.Client;

namespace Data.moduls;
public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public required string Location {get; set;}
  
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime ArrivedDate {get;set;}

    // relation with User where each product has been created only by 1 user
    public int UserId{get;set;}// foreign key 

    public  User User {get;set;} = null!;




}