using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;

public static class QuestPdfContentHelpers
{

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

    public static Image Base64ToImage(string base64String)
    {
        // Convert base 64 string to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        // Convert byte[] to Image
        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        {
            Image image = Image.FromStream(ms);
            return image;
        }
    }
}