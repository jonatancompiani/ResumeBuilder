using System.Collections.Generic;
using ResumeBuilder.Domain;

namespace ResumeBuilder.Application
{
    public interface IImageGenerator
    {
        IEnumerable<byte[]> GenerateImages(ResumeData resumeData);
    }
}
