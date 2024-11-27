namespace JobService.Infrastructure.Profiles;

public class ApplicantProfile : Profile
{
    public ApplicantProfile()
    {
        CreateMap<Applicant, ApplicantDto>();
        CreateMap<ApplicantDto, Applicant>();
    }
}