namespace JobService.Infrastructure.Services;
[AutoInterface]

public class ApplicantService : IApplicantService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;  

    public ApplicantService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ApplicantDto>> GetApplicants()
    {
        var applicants = await _context.Applicants.ToListAsync();
        return _mapper.Map<IEnumerable<ApplicantDto>>(applicants);
    }

    public async Task<ApplicantDto?> GetApplicant(int id)
    {
        var applicant = await _context.Applicants.FindAsync(id);
        return _mapper.Map<ApplicantDto>(applicant);
    }

    public async Task<Applicant> CreateApplicant(ApplicantDto applicantDto)
    {
        var applicant = _mapper.Map<Applicant>(applicantDto);
        _context.Applicants.Add(applicant);
        await _context.SaveChangesAsync();
        return applicant;
    }

    public async Task UpdateApplicant(int id, ApplicantDto applicantDto)
    {
        var applicant = await _context.Applicants.FindAsync(id);
        if (applicant == null) throw new Exception($"Applicant not found");
        applicant.Name = applicantDto.Name;
        applicant.Contacts = applicantDto.Contacts;
        applicant.Description = applicantDto.Description;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteApplicant(int id)
    {
        var applicant = await _context.Applicants.FindAsync(id);
        if (applicant != null)
        {
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
        }
    }
}