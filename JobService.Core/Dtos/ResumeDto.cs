namespace JobService.Core.DTOs;

public class ResumeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Skills { get; set; }
    public string Education { get; set; }
    public string Experience { get; set; }
    
    public int? ApplicantId { get; set; }
}