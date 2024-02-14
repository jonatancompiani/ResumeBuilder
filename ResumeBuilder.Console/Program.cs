using QuestPDF.Infrastructure;
using ResumeBuilder.Application;

QuestPDF.Settings.License = LicenseType.Community;

Generator gen = new(new(Theme.Cyan));
gen.PreviewFile();

