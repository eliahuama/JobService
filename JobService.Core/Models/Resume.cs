namespace JobService.Core.Models;
    
    public class Resume : BaseModel
    {
        public string Name { get; set; }
        public string Skills { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public Applicant Applicant { get; set; }
        public int ApplicantId { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<File> Files { get; set; }
    }
