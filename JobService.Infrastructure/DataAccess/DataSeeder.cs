namespace JobService.Infrastructure.DataAccess;

public class DataSeeder
{
    
    private readonly DataContext _context;

    public DataSeeder (DataContext context)
    {
        _context = context;
    }

    public void SeedDataContext()
    {
        if (!_context.Applicants.Any())
        {
            var applicants = new List<Applicant>
            {
                new Applicant
                {
                    Id = 1, Name = "Иван Иванов", Contacts = "ivan.ivanov@example.com",
                    Description = "Опытный разработчик ПО"
                },
                new Applicant
                {
                    Id = 2, Name = "Анна Смирнова", Contacts = "anna.smirnova@example.com",
                    Description = "Менеджер проектов с 10-летним стажем"
                }
            };
            _context.Applicants.AddRange(applicants);
        }

        if (!_context.Employers.Any())
        {
            var employers = new List<Employer>
            {
                new Employer { Id = 1, Name = "Tech Corp", Contacts = "contact@techcorp.com" },
                new Employer { Id = 2, Name = "Business Inc", Contacts = "info@businessinc.com" }
            };
            _context.Employers.AddRange(employers);
        }


        if (!_context.Jobs.Any())
        {
            var jobs = new List<Job>
            {
                new Job
                {
                    Id = 1, Title = "Разработчик ПО", Description = "Разработка и поддержка программных приложений",
                    Wage = 60000, Skills = "C#, .NET", ProgrammingLanguage = "C#", EmployerId = 1
                },
                new Job
                {
                    Id = 2, Title = "Менеджер проектов", Description = "Управление проектами по разработке ПО",
                    Wage = 80000, Skills = "Управление проектами, Agile", ProgrammingLanguage = "", EmployerId = 2
                }
            };
            _context.Jobs.AddRange(jobs);
        }

        if (!_context.Resumes.Any())
        {
            var resumes = new List<Resume>
            {
                new Resume
                {
                    Id = 1, Name = "Резюме Ивана Иванова", Skills = "C#, .NET, SQL",
                    Education = "Бакалавр компьютерных наук", Experience = "5 лет в Tech Corp", ApplicantId = 1
                },
                new Resume
                {
                    Id = 2, Name = "Резюме Анны Смирнова", Skills = "Управление проектами, Agile, Scrum",
                    Education = "MBA", Experience = "10 лет в Business Inc", ApplicantId = 2
                }
            };
            _context.Resumes.AddRange(resumes);
        }

        if (!_context.Files.Any())
        {
            var files = new List<File>
            {
                // new File { Id = 1, Path = "uploads/project_smirnova.pdf", ResumeId = 1 },
                // new File { Id = 2, Path = "uploads/project_smirnova.pdf", ResumeId = 2 }
            };
            _context.Files.AddRange(files);
        }

        _context.SaveChanges();
    }
}
