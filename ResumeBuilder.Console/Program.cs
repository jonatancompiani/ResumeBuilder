using QuestPDF.Infrastructure;
using ResumeBuilder.Application;
using System.Text.Json;

QuestPDF.Settings.License = LicenseType.Community;


var c = new Content(Theme.Green);

c.Name = "Potato Batata";

string serialized = JsonSerializer.Serialize(c);

Generator gen = new(c);
gen.PreviewFile();

