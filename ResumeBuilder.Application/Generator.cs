using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;

namespace ResumeBuilder.Application;

public class Generator
{
    Content _content;
    private Image _image;
    string _primaryColor;
    string _textColor;

    public Generator(Content content)
    {
        _content = content;
        _image = Image.FromFile(content.ImagePath);
        _primaryColor = content.PrimaryColor;
        _textColor = content.TextColor;
    }

    public byte[] GetFileBytes()
    {
        var document = GenerateDocument();

        return document.GeneratePdf();
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
                                                .Image(_image)
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
                                    .AlignLeft()
                                    .Text(_content.Name)
                                    .FontSize(22)
                                    .FontColor(_textColor)
                                    );

                                sidebar
                                    .Cell()
                                    .Row(3)
                                    .ShowOnce()
                                    .Element(container => container
                                            .AlignLeft()
                                            .Text(_content.Profession)
                                            .FontSize(16)
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
                                            .FontSize(16)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(5).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                sidebar
                                .Cell()
                                .Row(6)
                                .ShowOnce()
                                .Element(container => container
                                        .AlignLeft()
                                        .PaddingTop(10)
                                        .Text(_content.Phone)
                                        .FontSize(12)
                                        .FontColor(_textColor)
                                        );

                                sidebar
                                .Cell()
                                .Row(7)
                                .ShowOnce()
                                .Element(container => container
                                        .AlignLeft()
                                        .PaddingTop(10)
                                        .Text(_content.Email)
                                        .FontSize(12)
                                        .FontColor(_textColor)
                                        );

                                sidebar
                                    .Cell()
                                    .Row(8)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text(_content.HeaderSkills)
                                            .FontSize(16)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(9).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                sidebar
                                    .Cell()
                                    .Row(10)
                                    .ShowOnce()
                                    .Column(skills =>
                                    {
                                        foreach (var skill in _content.SkillList)
                                        {
                                            skills.Item().PaddingTop(5).Row(row =>
                                            {
                                                row.Spacing(5);
                                                row.AutoItem().Text($"-").FontColor(_textColor);
                                                row.RelativeItem().Text(skill).FontColor(_textColor);
                                            });
                                        }

                                    });

                                sidebar
                                    .Cell()
                                    .Row(11)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text(_content.HeaderLanguages)
                                            .FontSize(16)
                                            .FontColor(_textColor)
                                            );

                                sidebar.Cell().Row(12).ShowOnce().LineHorizontal(1).LineColor(_textColor);

                                sidebar
                                    .Cell()
                                    .Row(13)
                                    .ShowOnce()
                                    .Column(skills =>
                                    {
                                        foreach (var language in _content.LanguageList)
                                        {
                                            skills.Item().PaddingTop(5).Row(row =>
                                            {
                                                row.Spacing(5);
                                                row.AutoItem().Text($"-").FontColor(_textColor);
                                                row.RelativeItem().Text(language).FontColor(_textColor);
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
                                    .FontSize(20)
                                    );

                                body.Cell().Row(2).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                                body.Cell().Row(2).Element(container => container
                                                                    .AlignLeft()
                                                                    .PaddingBottom(10)
                                                                    .Text(_content.Summary)
                                                                    .FontSize(14)
                                                                    );

                                body.Cell().Row(3).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text(_content.HeaderExperience)
                                    .FontSize(20)
                                    );
                                body.Cell().Row(4).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                                body.Cell().Row(5).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text(_content.HeaderEducation)
                                    .FontSize(20)
                                    );
                                body.Cell().Row(6).ShowOnce().LineHorizontal(1).LineColor(_primaryColor);

                            }));
                });
            });
        });
    }
}
