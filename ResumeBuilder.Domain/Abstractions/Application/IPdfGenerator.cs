namespace ResumeBuilder.Domain.Abstractions.Application;

public interface IPdfGenerator
{
    byte[] GeneratePdf(ResumeData resumeData);
}
