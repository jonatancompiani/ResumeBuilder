namespace ResumeBuilder.Domain.dto;

public record ContentRequest(
    string Name,
    string? Base64avatar,
    string? ThemeColor,
    string? Profession,
    string? Address,
    string? Phone,
    string? Email,
    string? Linkedin,
    string? Github,
    string? Summary,
    List<string>? SkillList,
    List<Language>? LanguageList,
    List<WorkExperience>? WorkExperienceList,
    List<Education>? EducationList);