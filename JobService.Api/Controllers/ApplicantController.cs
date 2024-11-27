namespace JobService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]

public class ApplicantController : Controller
{
    private readonly IApplicantService _applicantService;

    public ApplicantController(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicants()
    {
        var applicants = await _applicantService.GetApplicants();
        return Ok(applicants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicant(int id)
    {
        var applicant = await _applicantService.GetApplicant(id);
        return Ok(applicant);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ApplicantDto applicantDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var applicant = await _applicantService.CreateApplicant(applicantDto);
        return Ok(applicant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ApplicantDto applicantDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var applicant = await _applicantService.GetApplicant(id);
        if (applicant is null) return NotFound();
        
        await _applicantService.UpdateApplicant(id, applicantDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var applicant = await _applicantService.GetApplicant(id);
        if (applicant is null) return NotFound();
        
        await _applicantService.DeleteApplicant(id);
        return NoContent();
    }
}
