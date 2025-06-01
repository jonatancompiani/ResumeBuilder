using ResumeBuilder.Domain.dto;

namespace ResumeBuilder.Domain.Abstractions.Application;

public interface IResumeDataProvider
{
    ResumeData CreateFromRequest(ContentRequest request);
    ResumeData CreateExample();
}
