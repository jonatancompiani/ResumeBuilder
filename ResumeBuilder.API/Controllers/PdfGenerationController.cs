using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfGenerationController : ControllerBase
{
    public PdfGenerationController() { }

    [HttpGet("Download")]
    public byte[] Get()
    {
        Generator gen = new(new(Theme.Blue));
        return gen.GetFileBytes();
    }

    [HttpGet("Colors")]
    public List<string> GetColors()
    {
        return Application.Content.ThemeColors.Values.ToList();
    }
}
