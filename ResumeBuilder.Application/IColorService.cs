using System.Collections.Generic;

namespace ResumeBuilder.Application
{
    public interface IColorService
    {
        IEnumerable<string> GetAvailableColors();
    }
}
