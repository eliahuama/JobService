namespace JobService.Api.Controllers;
[Route("api/[controller]")]
[ApiController]

public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IResumeService _resumeService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    } 
    
    [HttpPost]
    public async Task <IActionResult> UploadFile(int id, IFormFile? file)
    {
        if (file is null || file.Length is 0) return BadRequest("No file uploaded");
        if (!ExtensionHelper.GetAllowedExtensions().Contains(file.ContentType)) 
            return BadRequest("Extension is not allowed");
            
        await _fileService.FileUpload(id,file);
        
        return Ok("File uploaded successfully");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(int id)
    {
        var file = await _fileService.GetFileById(id);
        if(file == null) return NotFound();

        return Ok(file);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        var file = await _fileService.GetFileById(id);
        if (file == null) return NotFound();
        
        await _fileService.DeleteFile(id);
        return NoContent();
    }
}
