namespace JobService.Core.Models;
    
    public class Employer : BaseModel
    {
        public string Name { get; set; }
        public string Contacts {  get; set; }
        
        public string Description { get; set; }
        public ICollection<Job> Jobs { get; set; }
        [JsonIgnore]
        public LocalUser LocalUser { get; set; }
    }
