namespace JobService.Infrastructure.Services;
[AutoInterface]

public class ResumeService : IResumeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ResumeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResumeDto>> GetResumes()
    {
        var resumes = await _context.Resumes.ToListAsync();
        return _mapper.Map<IEnumerable<ResumeDto>>(resumes);
    }

    public async Task<ResumeDto?> GetResume(int id)
    {
        var resume = await _context.Resumes.FindAsync(id);
        return _mapper.Map<ResumeDto>(resume);
    }

    public async Task<Resume> CreateResume(ResumeDto resumeDto)
    {
        var resume = _mapper.Map<Resume>(resumeDto);
        _context.Resumes.Add(resume);
        await _context.SaveChangesAsync();
        return resume;
    }

    public async Task UpdateResume(int id, ResumeDto resumeDto)
    {
        var resume = await _context.Resumes.FindAsync(id);
        if (resume == null) throw new Exception($"Resume not found");
        resume.Name = resumeDto.Name;
        resume.Skills = resumeDto.Skills;
        resume.Education = resumeDto.Education;
        resume.Experience = resumeDto.Experience;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResume(int id)
    {
        var resume = await _context.Resumes.FindAsync(id);
        if (resume != null)
        {
            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
        }
    }
}