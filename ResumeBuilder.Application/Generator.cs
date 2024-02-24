using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;

namespace ResumeBuilder.Application;

public class Generator
{
    QuestPdfContent _content;
    string _primaryColor;
    string _textColor;

    public Generator(QuestPdfContent content)
    {
        _content = content;
        _primaryColor = content.PrimaryColor;
        _textColor = content.TextColor;
    }

    public static IEnumerable<string> GetAvailableColors()
    {
        return QuestPdfContentHelpers.ThemeColors.Values;
    }

    public byte[] GetFileBytes()
    {
        var document = GenerateDocument();

        return document.GeneratePdf();
    }

    public IEnumerable<byte[]> GetImageBytes()
    {
        var document = GenerateDocument();

        return document.GenerateImages();
    }

    public void PreviewFile()
    {
        var document = GenerateDocument();

        document.ShowInPreviewer(9090);
    }

    private IDocument GenerateDocument()
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
                        .BorderColor(_primaryColor)
                        .Background(_primaryColor)
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
                                    .Element(e =>
                                        e.Shrink()
                                        .Layers(layers =>
                                        {
                                            layers
                                            .PrimaryLayer()
                                                .Image(_content.b64avatarImg)
                                                .FitHeight();

                                            layers.Layer().Canvas((canvas, size) =>
                                            {
                                                canvas.DrawRoundRect(-15, -15, 150, 150, 80, 80, new SKPaint
                                                {
                                                    Color = SKColor.Parse(_primaryColor),
                                                    IsStroke = true,
                                                    StrokeWidth = 30,
                                                    IsAntialias = true
                                                });

                                            });


                                        }));

                                sidebar.Cell().Row(2).ShowOnce().Element(container => container
                                    .AlignCenter()
                                    .Text(_content.Name)
                                    .FontSize(24)
                                    .FontColor(_textColor)
                                    );

                                sidebar
                                    .Cell()
                                    .Row(3)
                                    .ShowOnce()
                                    .Element(container => container
                                            .AlignCenter()
                                            .Text(_content.Profession)
                                            .FontSize(12)
                                            .FontColor(_textColor)
                                            );
                                sidebar
                                    .Cell()
                                    .Row(4)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text(_content.HeaderContact)
                                            .FontSize(12)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(5).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                //sidebar.Cell().Row(6)
                                //.ShowOnce()
                                //.Element(container => container
                                //        .AlignLeft()
                                //        .PaddingTop(10)
                                //        .Text(_content.Phone)
                                //        .FontSize(12)
                                //        .FontColor(_textColor)
                                //        );

                                sidebar.Cell().Row(6).ShowOnce().Table(contactTable =>
                                {
                                    contactTable.ColumnsDefinition(c =>
                                    {
                                        c.ConstantColumn(10, Unit.Millimetre);
                                        c.RelativeColumn();
                                    });

                                    contactTable.Cell()
                                       .Row(1)
                                       .Column(1)
                                       .PaddingTop(10)
                                       .Height(18)
                                       .Image(_content.B64addressImg)
                                       .FitHeight();

                                    contactTable.Cell()
                                       .Row(2)
                                       .Column(1)
                                       .PaddingTop(10)
                                       .Height(18)
                                       .Image(_content.B64phoneImg)
                                       .FitHeight();

                                    contactTable.Cell()
                                       .Row(3)
                                       .Column(1)
                                       .PaddingTop(10)
                                       .Height(18)
                                       .Image(_content.B64emailImg)
                                       .FitHeight();

                                    contactTable.Cell()
                                       .Row(4)
                                       .Column(1)
                                       .PaddingTop(10)
                                       .Height(18)
                                       .Image(_content.B64linkedinImg)
                                       .FitHeight();

                                    contactTable.Cell()
                                      .Row(5)
                                      .Column(1)
                                      .PaddingTop(10)
                                      .Height(18)
                                      .Image(_content.B64githubImg)
                                      .FitHeight();

                                    contactTable.Cell()
                                        .Row(1)
                                        .Column(2)
                                        .PaddingTop(10)
                                        .Text(_content.Address)
                                        .FontSize(12)
                                        .FontColor(_textColor);

                                    contactTable.Cell()
                                        .Row(2)
                                        .Column(2)
                                        .PaddingTop(10)
                                        .Text(_content.Phone)
                                        .FontSize(12)
                                        .FontColor(_textColor);

                                    contactTable.Cell()
                                        .Row(3)
                                        .Column(2)
                                        .PaddingTop(10)
                                        .Text(_content.Email)
                                        .FontSize(12)
                                        .FontColor(_textColor);

                                    contactTable.Cell()
                                        .Row(4)
                                        .Column(2)
                                        .PaddingTop(10)
                                        .Text(_content.Linkedin)
                                        .FontSize(12)
                                        .FontColor(_textColor);

                                    contactTable.Cell()
                                        .Row(5)
                                        .Column(2)
                                        .PaddingTop(10)
                                        .Text(_content.Github)
                                        .FontSize(12)
                                        .FontColor(_textColor);
                                });

                                sidebar
                                    .Cell()
                                    .Row(7)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text(_content.HeaderSkills)
                                            .FontSize(12)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(8).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                sidebar
                                    .Cell()
                                    .Row(9)
                                    .ShowOnce()
                                    .Column(skills =>
                                    {
                                        foreach (var skill in _content.SkillList)
                                        {
                                            skills.Item().PaddingTop(5).Row(row =>
                                            {
                                                row.Spacing(5);
                                                row.AutoItem().Text($"•").FontSize(10).FontColor(_textColor);
                                                row.RelativeItem().Text(skill).FontSize(10).FontColor(_textColor);
                                            });
                                        }

                                    });

                                sidebar
                                    .Cell()
                                    .Row(10)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text(_content.HeaderLanguages)
                                            .FontSize(12)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(11).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                sidebar.Cell().Row(12).Table(langTable =>
                                {
                                    langTable.ColumnsDefinition(column => column.RelativeColumn());

                                    foreach (var language in _content.LanguageList)
                                    {
                                        langTable.Cell().PaddingTop(8).Table(x =>
                                        {
                                            x.ColumnsDefinition(column => column.RelativeColumn());

                                            x.Cell().Row(2).Text($"• {language.Name}").FontSize(10).FontColor(_textColor);
                                            x.Cell().Row(3).Text($"     {language.Level}").Light().FontSize(9).FontColor(_textColor);
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
                        .Element(e =>
                            e.Table(body =>
                            {
                                body.ColumnsDefinition(column => column.RelativeColumn());

                                body.Cell().Row(1).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text(_content.HeaderSummary)
                                    .FontSize(12)
                                    );

                                body.Cell().Row(2).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                                body.Cell().Row(2).Element(container => container
                                                                    .AlignLeft()
                                                                    .PaddingBottom(10)
                                                                    .Text(_content.Summary)
                                                                    .FontSize(12)
                                                                    );

                                body.Cell().Row(3).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text(_content.HeaderExperience)
                                    .FontSize(12)
                                    );
                                body.Cell().Row(4).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                                body.Cell().Row(5).Table(xpTable =>
                                {
                                    xpTable.ColumnsDefinition(column => column.RelativeColumn());

                                    foreach (var wex in _content.WorkExperienceList)
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

                                body.Cell().Row(6).Element(container => container
                                    .AlignLeft()
                                    .PaddingTop(10)
                                    .PaddingBottom(10)
                                    .Text(_content.HeaderEducation)
                                    .FontSize(12)
                                    );
                                body.Cell().Row(7).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                                body.Cell().Row(8).Table(xpTable =>
                                {
                                    xpTable.ColumnsDefinition(column => column.RelativeColumn());

                                    foreach (var edu in _content.EducationList)
                                    {
                                        xpTable.Cell().ShowEntire().Table(x =>
                                        {
                                            x.ColumnsDefinition(column => column.RelativeColumn());
                                            x.Cell().Row(1).PaddingTop(10).Text(edu.Year).Thin().FontSize(9);
                                            x.Cell().Row(2).Text(edu.Title).Bold();
                                            x.Cell().Row(3).Text(edu.Institution).Light();
                                            x.Cell().Row(4).Text(edu.Description);
                                        });
                                    }
                                });

                            }));
                });
            });
        });
    }
}
