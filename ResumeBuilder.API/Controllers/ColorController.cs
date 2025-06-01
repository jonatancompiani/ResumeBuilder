using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Domain.Abstractions.Application;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
[Tags("Color")]
public class ColorController(IColorService colorService) : ControllerBase
{
    [HttpGet("Colors")]
    public IEnumerable<string> GetColors()
    {
        return colorService.GetAvailableColors();
    }
}