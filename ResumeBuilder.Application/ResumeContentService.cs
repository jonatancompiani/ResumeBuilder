using QuestPDF.Helpers;
using ResumeBuilder.Domain;
using ResumeBuilder.Domain.Abstractions.Application;
using ResumeBuilder.Domain.dto;
using System.Globalization;

namespace ResumeBuilder.Application;
public class ResumeContentService : IResumeDataProvider, IColorService
{
    public ResumeData CreateFromRequest(ContentRequest request)
    {
        var resumeData = new ResumeData
        {
            Name = request.Name,
            Profession = request.Profession,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Linkedin = request.Linkedin,
            Github = request.Github,
            SkillList = request.SkillList,
            LanguageList = request.LanguageList,
            Summary = request.Summary,
            WorkExperienceList = request.WorkExperienceList,
            EducationList = request.EducationList,
            TextColor = Colors.White,

            HeaderContact = "Contact",
            HeaderSkills = "Skills",
            HeaderLanguages = "Languages",
            HeaderExperience = "Experience",
            HeaderEducation = "Education",
            HeaderSummary = "Summary"
        };

        if (!string.IsNullOrWhiteSpace(request.Base64avatar))
        {
            resumeData.AvatarImage = Convert.FromBase64String(request.Base64avatar);
        }

        if (request.ThemeColor is not null && QuestPdfContentHelpers.GetThemeColors().ContainsValue(request.ThemeColor))
        {
            resumeData.PrimaryColor = request.ThemeColor;
        }
        else
        {
            resumeData.PrimaryColor = QuestPdfContentHelpers.GetThemeColors()[Theme.Blue];
        }

        // Load icons
        resumeData.AddressIcon = Convert.FromBase64String(Base64Constants.b64addressStr);
        resumeData.EmailIcon = Convert.FromBase64String(Base64Constants.b64emailStr);
        resumeData.PhoneIcon = Convert.FromBase64String(Base64Constants.b64phoneStr);
        resumeData.LinkedinIcon = Convert.FromBase64String(Base64Constants.b64linkedinStr);
        resumeData.GithubIcon = Convert.FromBase64String(Base64Constants.b64githubStr);
        
        return resumeData;
    }

    public IEnumerable<string> GetAvailableColors()
    {
        return QuestPdfContentHelpers.GetThemeColors().Values;
    }

    public ResumeData CreateExample()
    {
        var faker = new Bogus.Faker("en");
        var name = faker.Name.FullName();
        var profession = faker.Name.JobTitle();
        var address = faker.Address.FullAddress();
        var phone = faker.Phone.PhoneNumber();
        var email = faker.Internet.Email();
        var linkedin = $"linkedin.com/in/{faker.Internet.UserName()}";
        var github = $"github.com/{faker.Internet.UserName()}";

        var skillList = faker.Make(6, () => faker.Hacker.Noun()).Distinct().ToList();

        var languageList = new List<Language>
        {
            new () { Name = "English", Level = "Fluent" },
            new () { Name = faker.Address.Country(), Level = "Beginner" }
        };

        var summary = faker.Lorem.Sentence(20);

        var workExperienceList = new List<WorkExperience>
        {
            new() {
                Company = faker.Company.CompanyName(),
                Role = "Senior " + faker.Name.JobTitle(),
                StartDate = faker.Date.Past(5).ToString("yyyy-MM"),
                EndDate = faker.Date.Recent(1).ToString("yyyy-MM"),
                Description = faker.Lorem.Paragraph()
            },
            new() {
                Company = faker.Company.CompanyName(),
                Role = faker.Name.JobTitle(),
                StartDate = faker.Date.Past(8, DateTime.Now.AddYears(-5)).ToString("yyyy-MM"),
                EndDate = faker.Date.Past(5).ToString("yyyy-MM"),
                Description = faker.Lorem.Paragraph()
            }
        };

        var educationList = new List<Education>
        {
            new() {
                Year = $"{faker.Date.Past(10, DateTime.Now.AddYears(-4)).Year} - {faker.Date.Past(4).Year}",
                Title = faker.Name.JobArea() + " Degree",
                Institution = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentence(10)
            }
        };

        return new ResumeData
        {
            Name = name,
            Profession = profession,
            Address = address,
            Phone = phone,
            Email = email,
            Linkedin = linkedin,
            Github = github,
            SkillList = skillList,
            LanguageList = languageList,
            Summary = summary,
            WorkExperienceList = workExperienceList,
            EducationList = educationList,
            AvatarImage = Convert.FromBase64String(Base64Constants.b64avatarStr),
            AddressIcon = Convert.FromBase64String(Base64Constants.b64addressStr),
            EmailIcon = Convert.FromBase64String(Base64Constants.b64emailStr),
            PhoneIcon = Convert.FromBase64String(Base64Constants.b64phoneStr),
            LinkedinIcon = Convert.FromBase64String(Base64Constants.b64linkedinStr),
            GithubIcon = Convert.FromBase64String(Base64Constants.b64githubStr),
            PrimaryColor = QuestPdfContentHelpers.GetThemeColors()[Theme.Blue],
            TextColor = Colors.White,
            HeaderContact = "Contact",
            HeaderSkills = "Skills",
            HeaderLanguages = "Languages",
            HeaderExperience = "Experience",
            HeaderEducation = "Education",
            HeaderSummary = "Summary"
        };
    }

}
