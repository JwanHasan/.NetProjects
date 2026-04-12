using System.Text;
using Data.moduls;
using Data.repo;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace Data.service;

public class UserService : IUserService
{

	private  IUserRepo  _userRepo;

	public UserService(IUserRepo userRepo)
	{
		_userRepo = userRepo;
	}

    public async Task<User> CreateUserAsync(UserDTO userDto, string username, string password, bool isAdmin)
    {	
		// 1. Check current user
    	User currentUser;
    	try
    	{
        	currentUser = await _userRepo.GetUserByUserNameAsync(userDto.Username);
    	}
    	catch
    	{
        throw new UnauthorizedAccessException("Current user not found");
    	}

    	if (!await _userRepo.isAdminAsync(currentUser.Id))
       	 throw new UnauthorizedAccessException("Only admin can create users");

    // 2. Validate inputs
    	if (string.IsNullOrWhiteSpace(username))
        	throw new ArgumentException("Username must be included");
    	if (string.IsNullOrWhiteSpace(password))
        	throw new ArgumentException("Password must be included");

    	if (await _userRepo.IsUsernameUsed(username))
       	 throw new ArgumentException("Username is already used");

    // 3. Hash password
    	string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);

    // 4. Create user object
   	 var newUser = new NewUser
    	{
        	UserName = username,
        	Password = passwordHash, // rename in entity
        	isAdmin = isAdmin
    	};

    // 5. Save to database
 	 await _userRepo.CreateUserAsync(newUser);
	var createdUser = await _userRepo.GetUserByUserNameAsync(username);


    // 6. Return created user
    return createdUser;


    }

    public async Task<bool> DeleteUserAsync(int id, UserDTO userDTO)
    {
		User currentUser;
    	try
    	{
        	currentUser = await _userRepo.GetUserByUserNameAsync(userDTO.Username);
    	}
    	catch
    	{
        throw new UnauthorizedAccessException("Current user not found");
    	}

    	if (!await _userRepo.isAdminAsync(currentUser.Id))
       	 throw new UnauthorizedAccessException("Only admin can delete users");

		await _userRepo.RemoveUserByIdAsync(id);

        return true;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
       var list = await _userRepo.GetAllUsersAsync();
	   return list;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        if(id<= 0 ) throw new ArgumentException(" the id must be bigger than 0");
		var foundUser = await _userRepo.GetUserByIdAsync(id);
		return foundUser;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        if(username.IsNullOrEmpty() || username.IsWhiteSpace() ) throw new ArgumentException(" the username must not be empty or has only spaces");
		var foundUser = await _userRepo.GetUserByUserNameAsync(username);
		return foundUser;
    }

    public async Task<User> UpdateUserPasswordAsync(UserDTO userDto,string password)
    {
       var findUser= await GetUserByUsernameAsync(userDto.Username);
	    bool verifyPassword = BCrypt.Net.BCrypt.Verify(userDto.Password,findUser.Password);
		if(!verifyPassword) throw new ArgumentException("the currect password and the saved password are not the same");

		var hashedpassword = BCrypt.Net.BCrypt.HashPassword(password);
		var result = await _userRepo.ChangeUserPasswordAsync(findUser.Id, hashedpassword);

		return result;

    }

   
}
