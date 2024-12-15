namespace JobService.Infrastructure.Services;

[AutoInterface]
public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly string? secretKey;

    public UserService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        secretKey = configuration.GetSection("Jwt")["Key"];
    }

    public async Task<bool> IsUserUnique(string username)
    {
        var user = await _context.LocalUsers.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null) return true;
        return false;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _context.LocalUsers.FirstOrDefaultAsync(x =>
            x.UserName.ToLower() == loginRequestDto.Username.ToLower() && x.Password == loginRequestDto.Password);
        if (user == null)
        {
            return new LoginResponseDto()
            {
                Token = string.Empty,
                User = null
            };
        }

        // generate JWT Token

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.EmployerId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = "JobService",
            Audience = "YourAudience",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var loginResponseDto = new LoginResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            User = user
        };
        return loginResponseDto;
    }

    public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
    {
        var employer = new Employer
        {
            Name = registrationRequestDto.Name,
            Contacts = registrationRequestDto.Contacts
        };

        _context.Employers.Add(employer);
        await _context.SaveChangesAsync();
        
        var user = new LocalUser
        {
            Name = registrationRequestDto.Name,
            Contacts = registrationRequestDto.Contacts,
            UserName = registrationRequestDto.UserName,
            Password = registrationRequestDto.Password,
            Role = registrationRequestDto.Role,
            Employer = employer
        };

        _context.LocalUsers.Add(user);
        await _context.SaveChangesAsync();

        user.Password = string.Empty;
        return user;
    }
}