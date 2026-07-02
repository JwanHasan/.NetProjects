namespace WereouseAPI;


using Microsoft.AspNetCore.Mvc;
using WereouseAPI.moduls;
using Data.service; // <-- your Data namespace  
using Data.moduls;

[ApiController]
[Route("api/users")]


public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] string adminUsername, [FromBody] WereouseAPI.moduls.UserDTO userDTO)
    {
        var newUser = new NewUser
        {
            UserName = userDTO.Username,
            Password = userDTO.Password,
            isAdmin = userDTO.IsAdmin
        };
        await _userService.CreateUserAsync(adminUsername, newUser);
        return Ok(userDTO);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromQuery] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]int id, [FromBody] Data.moduls.UserDTO userDTO)
 
    {
        await _userService.DeleteUserAsync(id, userDTO);
        return Ok();
    }
}