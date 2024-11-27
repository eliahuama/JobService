namespace JobService.Infrastructure.Services;
[AutoInterface]

public class JobService : IJobService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public JobService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JobDto>> GetJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();
        return _mapper.Map<IEnumerable<JobDto>>(jobs);
    }

    public async Task<JobDto?> GetJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        return _mapper.Map<JobDto>(job);
    }

    public async Task<Job> CreateJob(JobDto jobDto)
    {
        var job = _mapper.Map<Job>(jobDto);
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task UpdateJob(int id, JobDto jobDto)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null) throw new Exception($"Job not found");
        job.Title = jobDto.Title;
        job.Description = jobDto.Description;
        job.Wage = jobDto.Wage;
        job.Skills = jobDto.Skills;
        job.ProgrammingLanguage = jobDto.ProgrammingLanguage;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job != null)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }
}