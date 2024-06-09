using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;
using ResumeBuilder.Domain.dto;
using System.Diagnostics.CodeAnalysis;

namespace ResumeBuilder.Application;

public class QuestPdfContent
{
    public QuestPdfContent()
    {
        SetExample();
    }

    [SetsRequiredMembers] // To Avoid CS9035
    public QuestPdfContent(string color)
    {
        SetExample();

        PrimaryColor = 
            QuestPdfContentHelpers.ThemeColors.Any(x => x.Value == color) ? 
            color : 
            QuestPdfContentHelpers.ThemeColors[Theme.Blue];
    }

    [SetsRequiredMembers] // To Avoid CS9035
    public QuestPdfContent(ContentRequest content)
    {
        SetDefaults();
        Name = content.Name;

        if (!String.IsNullOrWhiteSpace(content.Base64avatar))
        {
            b64avatarImg = QuestPdfContentHelpers.Base64ToImage(content.Base64avatar);
        }

        PrimaryColor = QuestPdfContentHelpers.ThemeColors
            .FirstOrDefault(x => x.Value == content.ThemeColor).Value 
            ?? QuestPdfContentHelpers.ThemeColors[Theme.Blue];
        Profession = content.Profession;
        Address = content.Address;
        Phone = content.Phone;
        Email = content.Email;
        Linkedin = content.Linkedin;
        Github = content.Github;
        SkillList = content.SkillList;
        LanguageList = content.LanguageList;
        Summary = content.Summary;
        WorkExperienceList = content.WorkExperienceList;
        EducationList = content.EducationList;
    }

    public Image B64addressImg { get; private set; }
    public Image B64emailImg { get; private set; }
    public Image B64phoneImg { get; private set; }
    public Image B64linkedinImg { get; private set; }
    public Image B64githubImg { get; private set; }

    public string TextColor { get; set; }
    public string HeaderContact { get; set; }
    public string HeaderSkills { get; set; }
    public string HeaderLanguages { get; set; }
    public string HeaderExperience { get; set; }
    public string HeaderEducation { get; set; }
    public string HeaderSummary { get; set; }


    public Image? b64avatarImg { get; private set; }
    public string PrimaryColor { get; set; }
    public required string Name { get; set; }
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
    public List<Edcation>? EducationList { get; set; }

    private void SetDefaults()
    {
        B64addressImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64addressStr);
        B64emailImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64emailStr);
        B64phoneImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64phoneStr);
        B64linkedinImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64linkedinStr);
        B64githubImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64githubStr);

        TextColor ??= Colors.White;
        HeaderContact ??= @"Contact";
        HeaderSkills ??= @"Skills";
        HeaderLanguages ??= @"Languages";
        HeaderSummary ??= @"Carreer Objective";
        HeaderExperience ??= @"Experience";
        HeaderEducation ??= @"Education";
    }
    private void SetExample()
    {
        PrimaryColor = QuestPdfContentHelpers.ThemeColors[Theme.Blue];

        b64avatarImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64avatarStr);
        B64addressImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64addressStr);
        B64emailImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64emailStr);
        B64phoneImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64phoneStr);
        B64linkedinImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64linkedinStr);
        B64githubImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64githubStr);

        PrimaryColor ??= Colors.Blue.Darken4;
        TextColor ??= Colors.White;
        Name ??= @"John Doe";
        Profession ??= @"Software Developer";
        HeaderContact ??= @"Contact";
        Address ??= @"São Paulo";
        Phone ??= @"+123 456 789 012";
        Email ??= @"email.email@email.com";
        Linkedin ??= @"linkedin.com/in/john-doe/";
        Github ??= @"github.com/johndoe";
        HeaderSkills ??= @"Skills";
        SkillList ??= ["SQL Server", "Unity", "Advanced Excel", "Resilience", "Windows Server 2012"];
        HeaderLanguages ??= @"Languages";
        LanguageList ??=
        [
            new Language()
            {
                Name = "English",
                Level = "Advanced"
            },
            new Language()
            {
                Name = "Portuguese",
                Level = "Native"
            },
            new Language()
            {
                Name = "French",
                Level = "Basic"
            },
        ];
        HeaderSummary ??= @"Carreer Objective";
        Summary ??= @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        HeaderExperience ??= @"Experience";
        HeaderEducation ??= @"Education";
        WorkExperienceList ??=
        [
            new WorkExperience()
            {
                Company = "Microsoft",
                Role = "Software Developer",
                StartDate = "May/2022",
                EndDate = "Present",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            },
            new WorkExperience()
            {
                Company = "Ubisoft",
                Role = "QA",
                StartDate = "Jan/2020",
                EndDate = "May/2022",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            }
        ];
        EducationList ??=
        [
            new Edcation()
            {
                Year = "2019",
                Institution = "Harvard",
                Title = "Computer Science",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
            }
        ];
    }
}
