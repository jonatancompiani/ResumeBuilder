using ResumeBuilder.Domain;

namespace ResumeBuilder.Application
{
    public interface IPdfGenerator
    {
        byte[] GeneratePdf(ResumeData resumeData);
    }
}
