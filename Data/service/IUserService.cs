using Data.moduls;

namespace Data.service;
public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);

    // Commands
    Task<User> CreateUserAsync(UserDTO userDto, string username, string password, bool isAdmin);
    Task<User> UpdateUserPasswordAsync(UserDTO userDto, string password);

    Task<bool> DeleteUserAsync(int id,UserDTO userDTO);

   

}