using Data.moduls;

namespace Data.service;
public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);

    // Commands
    Task<User> CreateUserAsync(string adminUsername, NewUser newUser);
    Task<User> UpdateUserPasswordAsync(UserDTO userDto, string password);

    Task<bool> DeleteUserAsync(int id,UserDTO userDTO);

   

}