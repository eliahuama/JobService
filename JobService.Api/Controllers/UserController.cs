namespace JobService.Api.Controllers;

[Route("api/UsersAuthorization")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loginResponse = await _userService.Login(loginRequestDto);
        if (loginResponse.User == null)
        {
            return BadRequest("Username or password is incorrect.");
        }
        return Ok(loginResponse);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isUserUnique = await _userService.IsUserUnique(registrationRequestDto.UserName);
        if (isUserUnique == false)
        {
            return BadRequest("Username is already taken.");
        }
        var user = await _userService.Register(registrationRequestDto);
        return CreatedAtAction(nameof(Register), user);
    }
}