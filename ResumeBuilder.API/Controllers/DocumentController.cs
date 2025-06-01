using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Domain;
using ResumeBuilder.Domain.Abstractions.Application;
using ResumeBuilder.Domain.dto;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("[controller]")]
[Tags("Document")]
public class DocumentController(
    IResumeDataProvider resumeDataProvider,
    IPdfGenerator pdfGenerator,
    IImageGenerator imageGenerator) : ControllerBase
{
    [HttpPost("Preview")]
    [ProducesResponseType<FileContentResult>(200)]
    public ActionResult PostPreview([FromBody] ContentRequest content, [FromQuery] bool isMock = false)
    {
        try
        {
            ResumeData resumeData = isMock ?
                resumeDataProvider.CreateExample() :
                resumeDataProvider.CreateFromRequest(content);

            var imageBytes = imageGenerator.GenerateImages(resumeData);
            return Ok(imageBytes);
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }

    [HttpPost("Download")]
    [ProducesResponseType<FileContentResult>(200)]
    public ActionResult PostDownload([FromBody] ContentRequest content, [FromQuery] bool isMock = false)
    {
        try
        {
            ResumeData resumeData = isMock ?
                resumeDataProvider.CreateExample() :
                resumeDataProvider.CreateFromRequest(content);

            var pdfBytes = pdfGenerator.GeneratePdf(resumeData);
            return Ok(File(pdfBytes, "application/pdf", $"{content.Name}.pdf"));
        }
        catch (Exception ex)
        {
            return BadRequest($"Message: {ex.Message}\nInner: {ex.InnerException?.Message}\nStackTrace: {ex.StackTrace}");
        }
    }
}
