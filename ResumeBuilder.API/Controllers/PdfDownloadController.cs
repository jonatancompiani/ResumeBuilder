using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfDownloadController : ControllerBase
{
    public PdfDownloadController() { }

    [HttpGet(Name = "GetPdf")]
    public byte[] Get()
    {
        Generator gen = new();
        return gen.GenerateFile();
    }
}
