using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Application; // Ensures all interface usings are covered
using ResumeBuilder.Domain.dto;
using System; // For Exception
using System.Collections.Generic; // For IEnumerable

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DocController : ControllerBase
{
    private readonly IResumeDataProvider _resumeDataProvider;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IImageGenerator _imageGenerator;
    private readonly IColorService _colorService;

    public DocController(
        IResumeDataProvider resumeDataProvider,
        IPdfGenerator pdfGenerator,
        IImageGenerator imageGenerator,
        IColorService colorService)
    {
        _resumeDataProvider = resumeDataProvider;
        _pdfGenerator = pdfGenerator;
        _imageGenerator = imageGenerator;
        _colorService = colorService;
    }

    [HttpGet("ExampleDownload")]
    public IActionResult Get(string color) // Changed return type to IActionResult
    {
        try
        {
            var resumeData = _resumeDataProvider.CreateWithColor(color);
            var pdfBytes = _pdfGenerator.GeneratePdf(resumeData);
            // Consider returning File(pdfBytes, "application/pdf", "resume.pdf"); for better browser handling
            return Ok(pdfBytes);
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }

    [HttpGet("Colors")]
    public IEnumerable<string> GetColors()
    {
        return _colorService.GetAvailableColors();
    }

    [HttpGet("ExamplePreview")]
    [Tags("Example")]
    public ActionResult GetPreview(string color) // Changed to ActionResult
    {
        try
        {
            var resumeData = _resumeDataProvider.CreateWithColor(color);
            var imageBytes = _imageGenerator.GenerateImages(resumeData);
            return Ok(imageBytes);
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }

    [HttpPost("Preview")]
    public ActionResult PostPreview(ContentRequest content)
    {
        try
        {
            var resumeData = _resumeDataProvider.CreateFromRequest(content);
            var imageBytes = _imageGenerator.GenerateImages(resumeData);
            return Ok(imageBytes);
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
            var resumeData = _resumeDataProvider.CreateFromRequest(content);
            var pdfBytes = _pdfGenerator.GeneratePdf(resumeData);
            // Consider returning File(pdfBytes, "application/pdf", "resume.pdf");
            return Ok(pdfBytes);
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }
}
