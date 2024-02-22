using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DocController : ControllerBase
{
    public DocController() { }

    [HttpGet("Download")]
    public byte[] Get(string color)
    {
        var theme = Application.Content.ThemeColors.FirstOrDefault(x=> x.Value == color);
       
        Generator gen = new(new(theme.Key));
        return gen.GetFileBytes();
    }

    [HttpGet("Colors")]
    public List<string> GetColors()
    {
        return Application.Content.ThemeColors.Values.ToList();
    }

    [HttpGet("Preview")]
    public IEnumerable<byte[]> GetPreview(string color)
    {
        var theme = Application.Content.ThemeColors.FirstOrDefault(x => x.Value == color);

        Generator gen = new(new(theme.Key));
        return gen.GetImageBytes();
    }
}
