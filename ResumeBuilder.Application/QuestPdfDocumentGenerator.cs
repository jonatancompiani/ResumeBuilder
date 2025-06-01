using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ResumeBuilder.Domain; // For ResumeData
using SkiaSharp; // For SKPaint, SKColor if used directly in template
using QuestPDF.Helpers;
using ResumeBuilder.Application.Abstractions;

namespace ResumeBuilder.Application
{
    public class QuestPdfDocumentGenerator : IQuestPdfDocumentGenerator
    {
        public IDocument GenerateDocument(ResumeData resumeData)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Table(mainTable =>
                    {
                        mainTable.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                        });

                        mainTable
                            .Cell()
                            .Row(1)
                            .Column(1)
                            .Border(1)
                            .BorderColor(resumeData.PrimaryColor)
                            .Background(resumeData.PrimaryColor)
                            .ExtendVertical()
                            .Padding(15)
                            .Element(e => e
                                .Table(sidebar =>
                                {
                                    sidebar.ColumnsDefinition(cd => cd.RelativeColumn());

                                    sidebar.Cell()
                                        .Row(1)
                                        .ShowOnce()
                                        .AlignCenter()
                                        .AlignMiddle()
                                        .MaxHeight(120)
                                        .Element(element =>
                                        {
                                            var elem = element.Shrink();
                                            if (resumeData.AvatarImage is not null)
                                            {
                                                elem.Layers(layers =>
                                                {
                                                    layers
                                                    .PrimaryLayer()
                                                        .Image(resumeData.AvatarImage)
                                                        .FitHeight();

                                                    layers.Layer().Canvas((canvas, size) =>
                                                    {
                                                        canvas.DrawRoundRect(-15, -15, 150, 150, 80, 80, new SKPaint
                                                        {
                                                            Color = SKColor.Parse(resumeData.PrimaryColor),
                                                            IsStroke = true,
                                                            StrokeWidth = 30,
                                                            IsAntialias = true
                                                        });
                                                    });
                                                });
                                            }
                                        }
                                        );

                                    sidebar.Cell().Row(2).ShowOnce().Element(sContainer => sContainer
                                        .AlignCenter()
                                        .Text(resumeData.Name)
                                        .FontSize(24)
                                        .FontColor(resumeData.TextColor)
                                        );

                                    sidebar
                                        .Cell()
                                        .Row(3)
                                        .ShowOnce()
                                        .Element(sContainer => sContainer
                                                .AlignCenter()
                                                .Text(resumeData.Profession)
                                                .FontSize(12)
                                                .FontColor(resumeData.TextColor)
                                                );
                                    sidebar
                                        .Cell()
                                        .Row(4)
                                        .ShowOnce()
                                        .PaddingTop(20)
                                        .Element(sContainer => sContainer
                                                .AlignLeft()
                                                .PaddingBottom(10)
                                                .Text(resumeData.HeaderContact)
                                                .FontSize(12)
                                                .FontColor(resumeData.TextColor)
                                                );

                                    sidebar.Cell().Row(5).ShowOnce().LineHorizontal(1).LineColor(resumeData.TextColor);
                                    
                                    sidebar.Cell().Row(6).ShowOnce().Table(contactTable =>
                                    {
                                        contactTable.ColumnsDefinition(c =>
                                        {
                                            c.ConstantColumn(10, Unit.Millimetre);
                                            c.RelativeColumn();
                                        });

                                        if (!string.IsNullOrEmpty(resumeData.Address))
                                        {
                                            contactTable.Cell()
                                               .Row(1)
                                               .Column(1)
                                               .PaddingTop(10)
                                               .Height(18)
                                               .Image(resumeData.AddressIcon)
                                               .FitHeight();

                                            contactTable.Cell()
                                                .Row(1)
                                                .Column(2)
                                                .PaddingTop(10)
                                                .Text(resumeData.Address)
                                                .FontSize(9)
                                                .FontColor(resumeData.TextColor);
                                        }

                                        if (!string.IsNullOrEmpty(resumeData.Phone))
                                        {
                                            contactTable.Cell()
                                               .Row(2)
                                               .Column(1)
                                               .PaddingTop(10)
                                               .Height(18)
                                               .Image(resumeData.PhoneIcon)
                                               .FitHeight();

                                            contactTable.Cell()
                                                .Row(2)
                                                .Column(2)
                                                .PaddingTop(10)
                                                .Text(resumeData.Phone)
                                                .FontSize(9)
                                                .FontColor(resumeData.TextColor);
                                        }

                                        if (!string.IsNullOrEmpty(resumeData.Email))
                                        {
                                            contactTable.Cell()
                                               .Row(3)
                                               .Column(1)
                                               .PaddingTop(10)
                                               .Height(18)
                                               .Image(resumeData.EmailIcon)
                                               .FitHeight();

                                            contactTable.Cell()
                                                .Row(3)
                                                .Column(2)
                                                .PaddingTop(10)
                                                .Text(resumeData.Email)
                                                .FontSize(9)
                                                .FontColor(resumeData.TextColor);
                                        }

                                        if (!string.IsNullOrEmpty(resumeData.Linkedin))
                                        {
                                            contactTable.Cell()
                                               .Row(4)
                                               .Column(1)
                                               .PaddingTop(10)
                                               .Height(18)
                                               .Image(resumeData.LinkedinIcon)
                                               .FitHeight();

                                            contactTable.Cell()
                                                .Row(4)
                                                .Column(2)
                                                .PaddingTop(10)
                                                .Text(resumeData.Linkedin)
                                                .FontSize(9)
                                                .FontColor(resumeData.TextColor);
                                        }

                                        if (!string.IsNullOrEmpty(resumeData.Github))
                                        {
                                            contactTable.Cell()
                                              .Row(5)
                                              .Column(1)
                                              .PaddingTop(10)
                                              .Height(18)
                                              .Image(resumeData.GithubIcon)
                                              .FitHeight();
                                            
                                            contactTable.Cell()
                                                .Row(5)
                                                .Column(2)
                                                .PaddingTop(10)
                                                .Text(resumeData.Github)
                                                .FontSize(9)
                                                .FontColor(resumeData.TextColor);
                                        }
                                    });

                                    sidebar
                                        .Cell()
                                        .Row(7)
                                        .ShowOnce()
                                        .PaddingTop(20)
                                        .Element(sContainer => sContainer
                                                .AlignLeft()
                                                .PaddingBottom(10)
                                                .Text(resumeData.HeaderSkills)
                                                .FontSize(12)
                                                .FontColor(resumeData.TextColor)
                                                );

                                    sidebar.Cell().Row(8).ShowOnce().LineHorizontal(1).LineColor(resumeData.TextColor);

                                    sidebar
                                        .Cell()
                                        .Row(9)
                                        .ShowOnce()
                                        .Column(skills =>
                                        {
                                            foreach (var skill in resumeData.SkillList ?? new List<string>())
                                            {
                                                skills.Item().PaddingTop(5).Row(row =>
                                                {
                                                    row.Spacing(5);
                                                    row.AutoItem().Text($"•").FontSize(10).FontColor(resumeData.TextColor);
                                                    row.RelativeItem().Text(skill).FontSize(10).FontColor(resumeData.TextColor);
                                                });
                                            }
                                        });

                                    sidebar
                                        .Cell()
                                        .Row(10)
                                        .ShowOnce()
                                        .PaddingTop(20)
                                        .Element(sContainer => sContainer
                                                .AlignLeft()
                                                .PaddingBottom(10)
                                                .Text(resumeData.HeaderLanguages)
                                                .FontSize(12)
                                                .FontColor(resumeData.TextColor)
                                                );

                                    sidebar.Cell().Row(11).ShowOnce().LineHorizontal(1).LineColor(resumeData.TextColor);

                                    sidebar.Cell().Row(12).ShowOnce().Table(langTable =>
                                    {
                                        langTable.ColumnsDefinition(column => column.RelativeColumn());

                                        foreach (var language in resumeData.LanguageList ?? [])
                                        {
                                            langTable.Cell().PaddingTop(8).Table(x =>
                                            {
                                                x.ColumnsDefinition(column => column.RelativeColumn());
                                                x.Cell().Row(2).Text($"• {language.Name}").FontSize(10).FontColor(resumeData.TextColor);
                                                x.Cell().Row(3).Text($"     Level: {language.Level}").Light().FontSize(9).FontColor(resumeData.TextColor);
                                            });
                                        }
                                    });
                                }
                                ));

                        mainTable
                            .Cell()
                            .Row(1)
                            .Column(2)
                            .ShowOnce()
                            .Padding(15)
                            .Element(element =>
                                element.Table(body =>
                                {
                                    body.ColumnsDefinition(column => column.RelativeColumn());

                                    body.Cell().Row(1).Element(bContainer => bContainer
                                        .AlignLeft()
                                        .PaddingBottom(10)
                                        .Text(resumeData.HeaderSummary)
                                        .FontSize(12)
                                        .FontColor(Colors.Black)
                                        );

                                    body.Cell().Row(2).ShowOnce().LineHorizontal(1).LineColor(resumeData.PrimaryColor);

                                    body.Cell().Row(2).Element(bContainer => bContainer
                                                                        .AlignLeft()
                                                                        .PaddingBottom(10)
                                                                        .Text(resumeData.Summary)
                                                                        .FontSize(12)
                                                                        .FontColor(Colors.Black)
                                                                        );

                                    body.Cell().Row(3).Element(bContainer => bContainer
                                        .AlignLeft()
                                        .PaddingBottom(10)
                                        .Text(resumeData.HeaderExperience)
                                        .FontSize(12)
                                        .FontColor(Colors.Black)
                                        );
                                    body.Cell().Row(4).ShowOnce().LineHorizontal(1).LineColor(resumeData.PrimaryColor);

                                    body.Cell().Row(5).Table(xpTable =>
                                    {
                                        xpTable.ColumnsDefinition(column => column.RelativeColumn());

                                        foreach (var wex in resumeData.WorkExperienceList ?? [])
                                        {
                                            xpTable.Cell().ShowEntire().Table(x =>
                                            {
                                                x.ColumnsDefinition(column => column.RelativeColumn());
                                                x.Cell().Row(1).PaddingTop(10).Text($"{wex.StartDate} - {wex.EndDate}").Thin().FontSize(9);
                                                x.Cell().Row(2).Text(wex.Role).Bold();
                                                x.Cell().Row(3).Text(wex.Company).Light();
                                                x.Cell().Row(4).Text(wex.Description);
                                            });
                                        }
                                    });

                                    body.Cell().Row(6).Element(bContainer => bContainer
                                        .AlignLeft()
                                        .PaddingTop(10)
                                        .PaddingBottom(10)
                                        .Text(resumeData.HeaderEducation)
                                        .FontSize(12)
                                        .FontColor(Colors.Black)
                                        );
                                    body.Cell().Row(7).ShowOnce().LineHorizontal(1).LineColor(resumeData.PrimaryColor);

                                    body.Cell().Row(8).Table(eduTable =>
                                    {
                                        eduTable.ColumnsDefinition(column => column.RelativeColumn());

                                        foreach (var edu in resumeData.EducationList ?? [])
                                        {
                                            eduTable.Cell().ShowEntire().Table(x =>
                                            {
                                                x.ColumnsDefinition(column => column.RelativeColumn());
                                                x.Cell().Row(1).PaddingTop(10).Text(edu.Year).Thin().FontSize(9).FontColor(Colors.Black);
                                                x.Cell().Row(2).Text(edu.Title).Bold().FontColor(Colors.Black);
                                                x.Cell().Row(3).Text(edu.Institution).Light().FontColor(Colors.Black);
                                                x.Cell().Row(4).Text(edu.Description).FontColor(Colors.Black);
                                            });
                                        }
                                    });

                                }));
                    });
                });
            });
        }
    }
}
