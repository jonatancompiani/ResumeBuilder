namespace ResumeBuilder.Domain.Abstractions.Application;

public interface IColorService
{
    IEnumerable<string> GetAvailableColors();
}
