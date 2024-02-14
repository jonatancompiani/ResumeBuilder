using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ResumeBuilder.Application;

public class Content
{
    public Content(Theme theme)
    {
        PrimaryColor = ThemeColors[theme];
    }

    public Image addressImg = Image.FromFile(@"Resources\location.png");
    public Image emailImg = Image.FromFile(@"Resources\email.png");
    public Image phoneImg = Image.FromFile(@"Resources\phone.png");
    public Image linkedinImg = Image.FromFile(@"Resources\linkedin.png");
    public Image githubImg = Image.FromFile(@"Resources\github.png");

    public string PrimaryColor = Colors.Blue.Darken4;
    public string TextColor = Colors.White;
    public string ImagePath = @"Resources\avatar.jpg";
    public string Name = @"John Doe";
    public string Profession = @"Software Developer";
    public string HeaderContact = @"Contact";
    public string Address = @"São Paulo";
    public string Phone = @"+123 456 789 012";
    public string Email = @"email.email@email.com";
    public string Linkedin = @"linkedin.com/in/john-doe/";
    public string Github = @"github.com/johndoe";
    public string HeaderSkills = @"Skills";
    public List<string> SkillList = ["SQL Server", "Unity", "Advanced Excel", "Resilience", "Windows Server 2012"];
    public string HeaderLanguages = @"Languages";
    public List<Language> LanguageList =
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
    public string HeaderSummary = @"Carreer Objective";
    public string Summary = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    public string HeaderExperience = @"Experience";
    public string HeaderEducation = @"Education";
    public List<WorkExperience> WorkExperienceList =
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
    public List<Edcation> EducationList =
    [
        new Edcation()
        {
            Year = "2019",
            Institution = "Harvard",
            Title = "Computer Science",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        }
    ];

    public static Dictionary<Theme, string> ThemeColors =
        new()
        {
            { Theme.LightBlue, Colors.LightBlue.Darken4 },
            { Theme.Blue, Colors.Blue.Darken4 },
            { Theme.Cyan, Colors.Cyan.Darken4 },
            { Theme.Indigo, Colors.Indigo.Darken4 },
            { Theme.DeepPurple, Colors.DeepPurple.Darken4 },
            { Theme.Purple, Colors.Purple.Darken4 },
            { Theme.Lime, Colors.Lime.Darken4 },
            { Theme.LightGreen, Colors.LightGreen.Darken4 },
            { Theme.Green, Colors.Green.Darken4 },
            { Theme.Teal, Colors.Teal.Darken4 },
            { Theme.Yellow, Colors.Yellow.Darken4 },
            { Theme.Amber, Colors.Amber.Darken4 },
            { Theme.Orange, Colors.Orange.Darken4 },
            { Theme.DeepOrange, Colors.DeepOrange.Darken4 },
            { Theme.Red, Colors.Red.Darken4 },
            { Theme.Pink, Colors.Pink.Darken4 },
            { Theme.BlueGrey, Colors.BlueGrey.Darken4 },
            { Theme.Grey, Colors.Grey.Darken4 },
            { Theme.Brown, Colors.Brown.Darken4 },
            { Theme.Black, Colors.Black },
        };
}
