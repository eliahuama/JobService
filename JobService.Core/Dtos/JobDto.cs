namespace JobService.Core.DTOs;

public class JobDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Wage { get; set; }
    public string Skills { get; set; }
    public string ProgrammingLanguage { get; set; }
    public int? EmployerId { get; set; }
}