namespace Data.moduls;

public class NewUser
{
    public required string UserName {get;set;}
    public required string Password {get;set;} 
    public bool isAdmin {get;set;} = false;
}

