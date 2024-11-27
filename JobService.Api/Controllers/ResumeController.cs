namespace ItVacancies.Controllers;
[Route("api/[controller]")]
[ApiController]

public class ResumeController : Controller
{
    private readonly IResumeService _resumeService;

    public ResumeController(IResumeService resumeService)
    {
        _resumeService = resumeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resumes = await _resumeService.GetResumes();
        return Ok(resumes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resume = await _resumeService.GetResume(id);
        if (resume == null) return NotFound();
        
        return Ok(resume);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ResumeDto resumeDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var resume = await _resumeService.CreateResume(resumeDto);
        return Ok(resume);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ResumeDto resumeDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var existingResume = await _resumeService.GetResume(id);
        if (existingResume == null) return NotFound();

        await _resumeService.UpdateResume(id, resumeDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resume = await _resumeService.GetResume(id);
            if (resume == null) return NotFound();
            
            await _resumeService.DeleteResume(id);
            return NoContent(); 
        }
}