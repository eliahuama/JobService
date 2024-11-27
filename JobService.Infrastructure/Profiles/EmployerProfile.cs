namespace JobService.Infrastructure.Profiles;

public class EmployerProfile : Profile
{
    public EmployerProfile()
    {
        CreateMap<Employer, EmployerDto>();
        CreateMap<EmployerDto, Employer>();
    }
}