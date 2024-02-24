namespace ResumeBuilder.Domain.dto;

public class WorkExperience
{
    public required string Company { get; set; }
    public required string Role { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Description { get; set; }
}
