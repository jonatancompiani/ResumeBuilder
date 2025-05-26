using ResumeBuilder.Domain;
using ResumeBuilder.Domain.dto;

namespace ResumeBuilder.Application
{
    public interface IResumeDataProvider
    {
        ResumeData CreateFromRequest(ContentRequest request);
        ResumeData CreateExample();
        ResumeData CreateWithColor(string color);
    }
}
