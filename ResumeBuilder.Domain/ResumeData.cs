using QuestPDF.Infrastructure;
using System.Collections.Generic;
using ResumeBuilder.Domain.dto; // Assuming Language, WorkExperience, Education are here

namespace ResumeBuilder.Domain
{
    public class ResumeData
    {
        public string Name { get; set; }
        public string? Profession { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Linkedin { get; set; }
        public string? Github { get; set; }
        public List<string>? SkillList { get; set; }
        public List<Language>? LanguageList { get; set; }
        public string? Summary { get; set; }
        public List<WorkExperience>? WorkExperienceList { get; set; }
        public List<Education>? EducationList { get; set; } // Corrected typo Edcation to Education
        public Image? AvatarImage { get; set; }
        public Image AddressIcon { get; set; }
        public Image EmailIcon { get; set; }
        public Image PhoneIcon { get; set; }
        public Image LinkedinIcon { get; set; }
        public Image GithubIcon { get; set; }
        public string PrimaryColor { get; set; }
        public string TextColor { get; set; }
        public string HeaderContact { get; set; }
        public string HeaderSkills { get; set; }
        public string HeaderLanguages { get; set; }
        public string HeaderExperience { get; set; }
        public string HeaderEducation { get; set; }
        public string HeaderSummary { get; set; }
    }
}
