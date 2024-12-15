namespace JobService.Core.DTOs;

public class RegistrationRequestDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public RoleEnum Role { get; set; }
    public int? EmployerId { get; set; }
}