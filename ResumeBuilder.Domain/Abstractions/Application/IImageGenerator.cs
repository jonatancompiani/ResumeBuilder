namespace ResumeBuilder.Domain.Abstractions.Application;

public interface IImageGenerator
{
    IEnumerable<byte[]> GenerateImages(ResumeData resumeData);
}
