namespace JobService.Core.Models;
    
    public class Job : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Wage { get; set; }
        public string Skills { get; set; }
        public string ProgrammingLanguage { get; set; }
        public Employer Employer { get; set; }
        public int EmployerId { get; set; }
        public ICollection<Resume> Resumes { get; set; }
    }
