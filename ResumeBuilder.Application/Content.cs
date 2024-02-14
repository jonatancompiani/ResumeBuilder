using QuestPDF.Helpers;

namespace ResumeBuilder.Application;

public class Content
{
    public Content(Theme theme)
    {
        switch (theme)
        {
            case Theme.Blue:
                PrimaryColor = Colors.Blue.Darken4;
                break;

            case Theme.Red:
                PrimaryColor = Colors.Red.Darken4;
                break;

            case Theme.Green:
                PrimaryColor = Colors.Green.Darken4;
                break;

            case Theme.Black:
                PrimaryColor = Colors.Black;
                break;

            case Theme.Yellow:
                PrimaryColor = Colors.Yellow.Darken4;
                break;

            case Theme.Purple:
                PrimaryColor = Colors.Purple.Darken4;
                break;

            case Theme.Brown:
                PrimaryColor = Colors.Brown.Darken4;
                break;

            case Theme.DeepPurple:
                PrimaryColor = Colors.DeepPurple.Darken4;
                break;

            case Theme.Amber:
                PrimaryColor = Colors.Amber.Darken4;
                break;

            case Theme.BlueGrey:
                PrimaryColor = Colors.BlueGrey.Darken4;
                break;

            case Theme.Cyan:
                PrimaryColor = Colors.Cyan.Darken4;
                break;

            case Theme.DeepOrange:
                PrimaryColor = Colors.DeepOrange.Darken4;
                break;

            case Theme.Grey:
                PrimaryColor = Colors.Grey.Darken4;
                break;

            case Theme.Indigo:
                PrimaryColor = Colors.Indigo.Darken4;
                break;

            case Theme.LightBlue:
                PrimaryColor = Colors.LightBlue.Darken4;
                break;

            case Theme.LightGreen:
                PrimaryColor = Colors.LightGreen.Darken4;
                break;

            case Theme.Lime:
                PrimaryColor = Colors.Lime.Darken4;
                break;

            case Theme.Orange:
                PrimaryColor = Colors.Orange.Darken4;
                break;

            case Theme.Teal:
                PrimaryColor = Colors.Teal.Darken4;
                break;

            case Theme.Pink:
                PrimaryColor = Colors.Pink.Darken4;
                break;

        }
    }


    public string PrimaryColor = Colors.Blue.Darken4;
    public string TextColor = Colors.White;
    public string ImagePath = @".\avatar.jpg";
    public string Name = @"Name Surname";
    public string Profession = @"Profession";
    public string HeaderContact = @"Contact";
    public string Phone = @"+123 456 789 012";
    public string Email = @"email.email@email.com";
    public string HeaderSkills = @"Skills";
    public List<string> SkillList = ["Skill 1", "Skill 2", "Skill 3", "Skill 4", "Skill 5"];
    public string HeaderLanguages = @"Name Surname";
    public List<string> LanguageList = ["Language 1", "Language 2"];
    public string HeaderSummary = @"Carreer Objective";
    public string Summary = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    public string HeaderExperience = @"Experience";
    public string HeaderEducation = @"Education";

}
