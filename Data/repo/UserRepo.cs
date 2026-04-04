using System.Diagnostics;
using Data.moduls;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Data.repo;

public class UserRepo : IUserRepo
{
    private readonly AppDbContext _context;
    public UserRepo(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> ChangeUserPasswordAsync(int id, string password)
    {
        var foundUser = await GetUserByIdAsync(id);
        foundUser.Password = password;
        await _context.SaveChangesAsync();
        return foundUser;
    }

    public async Task ChangeUserRoleByUsernameAsync(string username, bool toAdmin )
    {
           var foundUser = await GetUserByUserNameAsync(username);
           foundUser.IsAdmin= toAdmin;
           await _context.SaveChangesAsync();
           System.Console.WriteLine($" the user {username} role is updated");
    }

    public async Task ChangeUserRoleByIdAsync(int id,bool toAdmin)
    {
       var founduser = await GetUserByIdAsync(id);
       founduser.IsAdmin = toAdmin;
       await _context.SaveChangesAsync();
       System.Console.WriteLine($" the user with id {id} admin role changed to {toAdmin}");
    }

    public async Task CreateUserAsync(NewUser newUser)
    {
        await _context.Users.AddAsync(new User{UserName= newUser.UserName, Password= newUser.Password});
        await _context.SaveChangesAsync();
        System.Console.WriteLine($"User with username {newUser.UserName} is created ");
        
    }


// the method get all users async can be sorted using linq for example this method
    public async Task<List<User>> GetAllUsersAsync()
    {
        var list = await _context.Users.OrderBy(x=> x.UserName).ToListAsync();
        return list;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var foundUser =await _context.Users.Include(x=> x.Products).FirstOrDefaultAsync(x=> x.Id == id)?? throw new Exception($"User with Id {id} does not exist");

        return foundUser;
    }

    public async Task<User> GetUserByUserNameAsync(string username)
    {
        var foundUser =await _context.Users.Include(x=> x.Products).FirstOrDefaultAsync(x=> x.UserName == username)?? throw new Exception($"User with username {username} does not exist");

        return foundUser;
    }

    public async Task RemoveUserByIdAsync(int id)
    {
        var removedUser = GetUserByIdAsync(id);
         _context.Remove(removedUser);
        await _context.SaveChangesAsync();
        System.Console.WriteLine("the user is removed");
    }

    public async Task RemoveUserByUserNameAsync(string username)
    {
        var removedUser = await GetUserByUserNameAsync(username);
         _context.Remove(removedUser);
        await _context.SaveChangesAsync();
        System.Console.WriteLine("the user is removed");
    }

    
}