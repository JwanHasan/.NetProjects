using System.ComponentModel;

namespace Data.moduls;
public class User
{
    public int Id {get;set;}
    public required string UserName {get;set;}
    public required string Password {get;set;}
    public bool IsAdmin {get;set;} = false;
}