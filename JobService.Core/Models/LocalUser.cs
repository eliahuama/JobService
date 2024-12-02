namespace JobService.Core.Models;

public class LocalUser : BaseModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
