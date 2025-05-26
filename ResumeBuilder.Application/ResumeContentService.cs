using ResumeBuilder.Domain;
using ResumeBuilder.Domain.dto;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Linq; // Required for .Contains
using System.Globalization; // Required for TextInfo
using System.Collections.Generic; // Required for IEnumerable

namespace ResumeBuilder.Application
{
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
                EducationList = request.EducationList, // Assumes ContentRequest.EducationList is List<Education>
                TextColor = Colors.White, // Default text color
                // Default Headers - can be localized or configured elsewhere if needed
                HeaderContact = "Contact",
                HeaderSkills = "Skills",
                HeaderLanguages = "Languages",
                HeaderExperience = "Experience",
                HeaderEducation = "Education",
                HeaderSummary = "Summary"
            };

            if (!string.IsNullOrWhiteSpace(request.Base64avatar))
            {
                resumeData.AvatarImage = QuestPdfContentHelpers.Base64ToImage(request.Base64avatar);
            }

            if (QuestPdfContentHelpers.ThemeColors.ContainsValue(request.ThemeColor))
            {
                resumeData.PrimaryColor = request.ThemeColor;
            }
            else
            {
                resumeData.PrimaryColor = QuestPdfContentHelpers.ThemeColors[Theme.Blue]; // Default theme color
            }

            // Load icons
            resumeData.AddressIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64addressStr);
            resumeData.EmailIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64emailStr);
            resumeData.PhoneIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64phoneStr);
            resumeData.LinkedinIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64linkedinStr);
            resumeData.GithubIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64githubStr);
            
            return resumeData;
        }

        public IEnumerable<string> GetAvailableColors()
        {
            return QuestPdfContentHelpers.ThemeColors.Values;
        }

        public ResumeData CreateExample()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return new ResumeData
            {
                Name = textInfo.ToTitleCase("john doe"),
                Profession = "Software Engineer",
                Address = "123 Main Street, Anytown, USA",
                Phone = "555-123-4567",
                Email = "john.doe@email.com",
                Linkedin = "linkedin.com/in/johndoe",
                Github = "github.com/johndoe",
                SkillList = new System.Collections.Generic.List<string> { "C#", ".NET", "Azure", "SQL", "JavaScript", "React" },
                LanguageList = new System.Collections.Generic.List<Language>
                {
                    new Language { Name = "English", Level = 5 },
                    new Language { Name = "Spanish", Level = 3 }
                },
                Summary = "A highly motivated and results-oriented software engineer with 5+ years of experience in developing and implementing innovative software solutions. Proven ability to work independently and as part of a team to deliver high-quality products on time and within budget.",
                WorkExperienceList = new System.Collections.Generic.List<WorkExperience>
                {
                    new WorkExperience
                    {
                        Year = "2020 - Present",
                        JobTitle = "Senior Software Engineer",
                        Company = "Tech Solutions Inc.",
                        Description = "Led the development of a new cloud-based platform, resulting in a 20% increase in efficiency. Mentored junior engineers and contributed to code reviews."
                    },
                    new WorkExperience
                    {
                        Year = "2018 - 2020",
                        JobTitle = "Software Engineer",
                        Company = "Web Innovations Co.",
                        Description = "Developed and maintained web applications using .NET and React. Collaborated with cross-functional teams to define project requirements and deliver solutions."
                    }
                },
                EducationList = new System.Collections.Generic.List<Education>
                {
                    new Education
                    {
                        Year = "2014 - 2018",
                        Title = "Bachelor of Science in Computer Science",
                        Institution = "University of Technology"
                    }
                },
                AvatarImage = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64avatarStr),
                AddressIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64addressStr),
                EmailIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64emailStr),
                PhoneIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64phoneStr),
                LinkedinIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64linkedinStr),
                GithubIcon = QuestPdfContentHelpers.Base64ToImage(Base64Constants.b64githubStr),
                PrimaryColor = QuestPdfContentHelpers.ThemeColors[Theme.Blue],
                TextColor = Colors.White,
                HeaderContact = "Contact",
                HeaderSkills = "Skills",
                HeaderLanguages = "Languages",
                HeaderExperience = "Experience",
                HeaderEducation = "Education",
                HeaderSummary = "Summary"
            };
        }

        public ResumeData CreateWithColor(string color)
        {
            var resumeData = CreateExample(); // Start with example data

            if (QuestPdfContentHelpers.ThemeColors.ContainsValue(color))
            {
                resumeData.PrimaryColor = color;
            }
            else
            {
                resumeData.PrimaryColor = QuestPdfContentHelpers.ThemeColors[Theme.Blue]; // Default if color is invalid
            }
            return resumeData;
        }
    }
}
