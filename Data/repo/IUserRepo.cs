using Data.moduls;

namespace Data.repo;
public interface IUserRepo
{
     Task CreateUserAsync(NewUser newUser);
     Task<User> GetUserByIdAsync(int id);
     Task<User> GetUserByUserNameAsync(string username);
     Task<List<User>> GetAllUsersAsync();
     Task ChangeUserRoleByIdAsync(int id, bool toAdmin);
     Task ChangeUserRoleByUsernameAsync(string username, bool toAdmin );
     Task<User> ChangeUserPasswordAsync(int id, string password);
     Task RemoveUserByIdAsync(int id);
     Task RemoveUserByUserNameAsync(string username);
     Task<bool> isAdminAsync(int userId);

     Task<bool> IsUsernameUsed(string username);
}

