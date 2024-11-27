namespace JobService.Infrastructure.Services;
[AutoInterface]

public class EmployerService : IEmployerService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public EmployerService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployerDto>> GetEmployers()
    {
        var employers = await _context.Employers.ToListAsync();
        return _mapper.Map<IEnumerable<EmployerDto>>(employers);
    }

    public async Task<EmployerDto?> GetEmployer(int id)
    {
        var employer = await _context.Employers.FindAsync(id);
        return _mapper.Map<EmployerDto>(employer);
    }

    public async Task<Employer> CreateEmployer(EmployerDto employerDto)
    {
        var employer = _mapper.Map<Employer>(employerDto);
        _context.Employers.Add(employer);
        await _context.SaveChangesAsync();
        return employer;
    }

    public async Task UpdateEmployer(int id, EmployerDto employerDto)
    {
        var employer = await _context.Employers.FindAsync(id);
        if (employer == null) throw new Exception($"Employer not found");
        employer.Name = employerDto.Name;
        employer.Contacts = employerDto.Contacts;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployer(int id)
    {
        var employer = await _context.Employers.FindAsync(id);
        if (employer != null)
        {
            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();
        }
    }
}