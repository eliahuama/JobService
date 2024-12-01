namespace JobService.Api.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJob(id);
            if (job == null) return NotFound();
            
            return Ok(job);  
        }

        [HttpPost]

        public async Task<IActionResult> Create (JobDto jobDto)
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // jobDto.EmployerId = 1;
            var job = await _jobService.CreateJob(jobDto);
            return Ok(job);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, JobDto jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingJob = await _jobService.GetJob(id);
            if (existingJob == null) return NotFound();

            await _jobService.UpdateJob(id, jobDto);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete (int id)
        {
            var job = await _jobService.GetJob(id);
            if (job == null) return NotFound();
            
            await _jobService.DeleteJob(id);
            return NoContent();
        }
    }
