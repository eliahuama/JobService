namespace JobService.Api.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]

        public async Task<IActionResult>GetJobs ()
        {
            var jobs = await _jobService.GetJobs();
            return Ok(jobs);
        }

        [HttpGet("employer/{employerId}")]
        public async Task<IActionResult> GetJobsByEmployerId(int employerId)
        {
            var jobs = await _jobService.GetJobsByEmployerId(employerId);
            return Ok(jobs);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJob(id);
            if (job == null) return NotFound();
            
            return Ok(job);  
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Create (JobDto jobDto)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var employerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(employerId, out var employerIdInt)) 
                jobDto.EmployerId = employerIdInt; 
            else  
                throw new Exception("Unable to convert Employer ID to int."); 

            var job = await _jobService.CreateJob(jobDto);
            return Ok(job);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Update(int id, JobDto jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingJob = await _jobService.GetJob(id);
            if (existingJob == null) return NotFound();

            await _jobService.UpdateJob(id, jobDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Delete (int id)
        {
            var job = await _jobService.GetJob(id);
            if (job == null) return NotFound();
            
            await _jobService.DeleteJob(id);
            return NoContent();
        }
    }
