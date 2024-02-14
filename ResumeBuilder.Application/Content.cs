using QuestPDF.Helpers;

namespace ResumeBuilder.Application;

public class Content
{
    public Content(Theme theme)
    {
        PrimaryColor = ThemeColors[theme];
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

    public static Dictionary<Theme, string> ThemeColors = 
        new() 
        { 
            { Theme.Blue, Colors.Blue.Darken4 }, 
            { Theme.Red, Colors.Red.Darken4 }, 
            { Theme.Green, Colors.Green.Darken4 }, 
            { Theme.Black, Colors.Black }, 
            { Theme.Yellow, Colors.Yellow.Darken4 }, 
            { Theme.Purple, Colors.Purple.Darken4 }, 
            { Theme.Brown, Colors.Brown.Darken4 }, 
            { Theme.DeepPurple, Colors.DeepPurple.Darken4 }, 
            { Theme.Amber, Colors.Amber.Darken4 }, 
            { Theme.BlueGrey, Colors.BlueGrey.Darken4 }, 
            { Theme.Cyan, Colors.Cyan.Darken4 }, 
            { Theme.DeepOrange, Colors.DeepOrange.Darken4 }, 
            { Theme.Grey, Colors.Grey.Darken4 }, 
            { Theme.Indigo, Colors.Indigo.Darken4 }, 
            { Theme.LightBlue, Colors.LightBlue.Darken4 }, 
            { Theme.LightGreen, Colors.LightGreen.Darken4 }, 
            { Theme.Lime, Colors.Lime.Darken4 }, 
            { Theme.Orange, Colors.Orange.Darken4 }, 
            { Theme.Teal, Colors.Teal.Darken4 }, 
            { Theme.Pink, Colors.Pink.Darken4 }
        };
}
