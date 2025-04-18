using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application;
using ResumeBuilder.Domain.dto;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DocController : ControllerBase
{
    public DocController() { }

    [HttpGet("ExampleDownload")]
    public byte[] Get(string color)
    {
        Generator gen = new(new(color));
        return gen.GetFileBytes();
    }
    
    [HttpGet("Colors")]
    public IEnumerable<string> GetColors()
    {
        return Generator.GetAvailableColors();
    }

    [HttpGet("ExamplePreview")]
    [Tags("Example")]
    public IEnumerable<byte[]> GetPreview(string color)
    {
        Generator gen = new(new(color));
        return gen.GetImageBytes();
    }

    [HttpPost("Preview")]
    public ActionResult PostPreview(ContentRequest content)
    {
        try
        {
            QuestPdfContent c = new(content);
            Generator gen = new(c);
            return Ok(gen.GetImageBytes());
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }


    [HttpPost("Download")]
    public ActionResult PostDownload(ContentRequest content)
    {
        try
        {
            QuestPdfContent c = new(content);
            Generator gen = new(c);
            return Ok(gen.GetFileBytes());
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }
}
