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
    public QuestPdfContent(string? color)
    {
        SetExample();

        PrimaryColor = 
            QuestPdfContentHelpers.ThemeColors.Any(x => x.Value.Equals(color)) ? 
            color! : 
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
    private void SetCV()
    {
        PrimaryColor = QuestPdfContentHelpers.ThemeColors[Theme.Blue];

        b64avatarImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64photoStr);
        B64addressImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64addressStr);
        B64emailImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64emailStr);
        B64phoneImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64phoneStr);
        B64linkedinImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64linkedinStr);
        B64githubImg = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64githubStr);

        PrimaryColor ??= Colors.Blue.Darken4;
        TextColor ??= Colors.White;
        Name ??= @"Jonatan Compiani";
        Profession ??= @"Backend Software Engineer";
        HeaderContact ??= @"Contact";
        Address ??= @"Brusque/SC - Brazil";
        Phone ??= @"+55 14 99841-0319";
        Email ??= @"jonatan.compiani@gmail.com";
        Linkedin ??= @"jonatan-compiani";
        Github ??= @"jonatancompiani";
        HeaderSkills ??= @"Skills";
        SkillList ??= ["C#", "SQL Server", ".NET Core", "Docker", "Kafka"];
        HeaderLanguages ??= @"Languages";
        LanguageList ??=
        [
            new Language()
            {
                Name = "English",
                Level = "Fluent"
            },
            new Language()
            {
                Name = "Portuguese",
                Level = "Native"
            }
        ];
        HeaderSummary ??= @"Summary";
        Summary ??= @"As an accomplished Senior Software Engineer with over 10 years of hands-on experience in technologies such as .Net, C#, SQL Server, Azure DevOps, Angular, and Bootstrap, I bring a wealth of expertise to any software development project. My proficiency extends to web applications, Windows services, and Rest APIs, and I have a proven track record of seamlessly transitioning into Scrum Master roles when needed. I thrive in dynamic, diverse work environments where collaboration fuels innovation, and my unwavering commitment to excellence drives continuous improvement.";
        HeaderExperience ??= @"Experience";
        HeaderEducation ??= @"Education";
        WorkExperienceList ??=
        [
            new WorkExperience()
            {
                Company = "SoftDesign • Remote",
                Role = "Software Developer",
                StartDate = "Feb 2024",
                EndDate = "Present",
                Description = @"Working on a project as the only backend developer for the project, following the best code standards such as SOLID, DRY, YAGNI, KISS, etc."
            },
            new WorkExperience()
            {
                Company = "FARFETCH Platform Solutions • Porto, Portugal",
                Role = "Software Engineer - Backend",
                StartDate = "Apr 2022",
                EndDate = "Apr 2024",
                Description = @"Working on a microservices heavy environment, developing new services and maintaining old ones, collaborating with other teams to achieve the best solution meeting the business needs.

Technologies: Docker; .Net 6, MongoDB, Kafka, Swagger (API Documentation), Jenkins (Pipelines), GitLab (Versoning), Jira (Board/Tasks tracking), etc."
            },
            new WorkExperience()
            {
                Company = "Philips • Remote",
                Role = "Senior Software Development Analyst",
                StartDate = "Feb 2022",
                EndDate = "Apr 2022",
                Description = @"Responsibilities: Leading the development, refining the items with the team, aligning expectations with the Product Owner, helping other team members with technical and requirement doubts and ensure the correct development following the quality standards required by performing code review, doing Pair Programming whenever required, and ensure the correct implementation of the unit tests.

Technologies: .Net Framework 4.6.2, T-SQL, WPF, xUnit, Azure Pipelines, Jenkins"
            },
            new WorkExperience()
            {
                Company = "Philips • Remote",
                Role = "Software Development Analyst",
                StartDate = "May 2021",
                EndDate = "Jan 2022",
                Description = @"I joined the company as part of a team responsible of maintenance and bugfixes for the Cardiology Informatics products and tools. I took part on the creation of a new team responsible for the serviceability of some products.

Highlighted projects: a tool used for system health verification before and after product installation, and the international collaboration for the development of a proactive monitoring tool.

Technologies: C# 8.0, .Net Core 3.1, .Net Framework 4.8, Powershell.
Tools: Visual Studio 2019, Prometheus, Grafana"
            },
            new WorkExperience()
            {
                Company = "Tata Consultancy Services • Londrina, Brazil",
                Role = "Senior Software Engineer",
                StartDate = "Oct 2020",
                EndDate = "May 2021",
                Description = @"I've had the privilege of working on software development projects for the Brazilian branch of SwissRe, one of the world's leading insurance companies. My role has been multifaceted, encompassing analysis of requirements, suggesting value-enhancing solutions by aligning with business needs, and ensuring the development of high-performance code.

In addition to my development responsibilities, I actively support team members in problem-solving and knowledge sharing, performing Pull Request reviews of a team of 20+ developers, and ensuring adherence to architectural standards. I utilize Azure DevOps to manage pipelines and releases for pre-UAT environments, facilitating efficient release management.

Furthermore, I serve as the Scrum Master for one of the five existing squads, orchestrating ceremonies and driving continuous value delivery to our clients.

The technologies I've worked with include C#, .NET 4.6, and SQL Server 2016 for back-end development, and for the front-end, I've employed JavaScript, AngularJS, Bootstrap, Razor, and JQuery.

I'm well-versed in utilizing tools such as Azure DevOps (Azure Boards, Azure Repos, Azure Pipelines), Git, Visual Studio 2019, SQL Server Management Studio, SoapUI, and Rabbit MQ. It's worth noting that my role often involves maintaining legacy applications, showcasing my adaptability and versatility in software development"
            },
            new WorkExperience()
            {
                Company = "Tata Consultancy Services • São Paulo, Brazil",
                Role = "Software Engineer",
                StartDate = "Jan 2017",
                EndDate = "Oct 2020",
                Description = @"I played a central role in the development and maintenance of insurance software, involving:

- Developing and maintaining APIs and Single Page Applications.
- Crafting user-friendly front-end solutions using AngularJS, Angular2+ and Bootstrap.
- Building a robust back-end with the C# language.
- Efficiently managing the SQL Server database.
- Ensuring top-notch code quality through comprehensive reviews.
- Taking charge of Scrum ceremonies for my squad (~5 people)."
            },
            new WorkExperience()
            {
                Company = "Tata Consultancy Services • Bangalore, India",
                Role = "Software Development Intern",
                StartDate = "Jan 2016",
                EndDate = "Dec 2016",
                Description = @"In my role as a Portuguese-speaking .Net developer, I undertook several key responsibilities. Primarily, I was tasked with the development and maintenance of the client's insurance application, both on the front-end and back-end. I also managed the SQL database, ensuring data accuracy and consistency. In addition to my technical duties, I played a vital role in bridging the cultural gap between team members and our Brazilian clients, facilitating effective communication and collaboration."
            },
            new WorkExperience()
            {
                Company = "Bouton Industria E Comercio De Artigos De Cama E Banho • Brusque, Brazil",
                Role = "Junior Software Developer",
                StartDate = "Nov 2013",
                EndDate = "Dec 2015",
                Description = "Designed and built solutions using WPF and Visual Studio, enhancing user interfaces and internal data management, provided ongoing application support, and managed the SQL Server database. Spearheaded the development of new system functionalities, contributing to overall system growth and improvement."
            }
        ];
        EducationList ??=
        [
            new Edcation()
            {
                Year = "2020",
                Institution = "Unifil - Centro Universitátio Filadélfia • Londrina, Brazil",
                Title = "Bachelor's Degree in Computer Science",
                Description = @"Activities and societies: In the final stage of the graduation I have written a paper, where I have implemented and tested different convolutional neural networks applied to image recognition, the aim was to know which type of banana was present in the picture.

I created the dataset, and configured the neural networks manually using Tensorflow and Google Colab, where I measured the effectivity of this approach, reaching a best scenario of 85% of success."
            }
        ];
    }
}
