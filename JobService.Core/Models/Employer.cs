namespace JobService.Core.Models;
    
    public class Employer : BaseModel
    {
        public string Name { get; set; }
        public string Contacts {  get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
