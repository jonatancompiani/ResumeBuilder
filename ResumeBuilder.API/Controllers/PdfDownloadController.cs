using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfDownloadController : ControllerBase
{
    public PdfDownloadController() { }

    [HttpGet]
    public byte[] Get()
    {
        Generator gen = new();
        return gen.GetFileBytes();
    }
}
