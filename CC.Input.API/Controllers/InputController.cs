using Microsoft.AspNetCore.Mvc;

namespace CC.Input.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InputController : ControllerBase
{
    private readonly ILogger<InputController> _logger;

    private readonly Logic.IRepository<Logic.Model.Input> _repository;

    public InputController(
        ILogger<InputController> logger,
        Logic.IRepository<Logic.Model.Input> repository
        )
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.RetrieveAllAsync());
    }

    [HttpPost]
    [Route("upload")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
    public async Task<IActionResult> Upload()
    {
        if (!Request.Form.Files.Any() | Request.Form.Files.Count>1)
            return BadRequest("Only one file allowed");
        bool commit = false;
        if (Request.Form.Keys.Contains("commit"))
            commit = Request.Form["commit"] == "True" ? true : false;
        else
            return BadRequest($@"Missing boolean form variable 'Commit' which should be 'True' if file content is valid and you wish to persist it's contents.");

        IFormFile file = Request.Form.Files[0];
        await using MemoryStream memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        string content = new StreamReader(memoryStream).ReadToEnd();

        return Ok(await _repository.UploadAsync(content,commit));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        await _repository.DeleteAllAsync();
        return Ok();
    }
}

