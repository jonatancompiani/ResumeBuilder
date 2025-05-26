namespace ResumeBuilder.Domain.dto;

public class Education
{
    public string? Year { get; set; }
    public string? Institution { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
}
