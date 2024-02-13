using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;

namespace ResumeBuilder.Application;

public class Generator
{
    string sidebarBackgroundColor = Colors.Blue.Darken4;
    string sidebarTextColor = Colors.White;

    Image image = Image.FromFile(@".\avatar.jpg");

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
                        .BorderColor(sidebarBackgroundColor)
                        .Background(sidebarBackgroundColor)
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
                                                .Image(image)
                                                .FitHeight();

                                            layers.Layer().Canvas((canvas, size) =>
                                            {
                                                canvas.DrawRoundRect(-15, -15, 150, 150, 80, 80, new SKPaint
                                                {
                                                    Color = SKColor.Parse(sidebarBackgroundColor),
                                                    IsStroke = true,
                                                    StrokeWidth = 30,
                                                    IsAntialias = true
                                                });

                                            });


                                        }));

                                sidebar.Cell().Row(2).ShowOnce().Element(container => container
                                    .AlignLeft()
                                    .Text("Name Surname")
                                    .FontSize(22)
                                    .FontColor(sidebarTextColor)
                                    );

                                sidebar
                                    .Cell()
                                    .Row(3)
                                    .ShowOnce()
                                    .Element(container => container
                                            .AlignLeft()
                                            .Text("Profession")
                                            .FontSize(16)
                                            .FontColor(sidebarTextColor)
                                            );
                                sidebar
                                    .Cell()
                                    .Row(4)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text("Contact")
                                            .FontSize(16)
                                            .FontColor(sidebarTextColor)
                                            );

                                sidebar.Cell().Row(5).ShowOnce().LineHorizontal(1).LineColor(sidebarTextColor);

                                sidebar
                                .Cell()
                                .Row(6)
                                .ShowOnce()
                                .Element(container => container
                                        .AlignLeft()
                                        .PaddingTop(10)
                                        .Text("+123 456 789 012")
                                        .FontSize(12)
                                        .FontColor(sidebarTextColor)
                                        );

                                sidebar
                                .Cell()
                                .Row(7)
                                .ShowOnce()
                                .Element(container => container
                                        .AlignLeft()
                                        .PaddingTop(10)
                                        .Text("email.email@email.com")
                                        .FontSize(12)
                                        .FontColor(sidebarTextColor)
                                        );

                                sidebar
                                    .Cell()
                                    .Row(8)
                                    .ShowOnce()
                                    .PaddingTop(20)
                                    .Element(container => container
                                            .AlignLeft()
                                            .PaddingBottom(10)
                                            .Text("Skills")
                                            .FontSize(16)
                                            .FontColor(sidebarTextColor)
                                            );

                                sidebar.Cell().Row(9).ShowOnce().LineHorizontal(1).LineColor(sidebarTextColor);

                                sidebar
                                    .Cell()
                                    .Row(10)
                                    .ShowOnce()
                                    .Column(skills =>
                                    {
                                        foreach (var i in Enumerable.Range(1, 5))
                                        {
                                            skills.Item().PaddingTop(5).Row(row =>
                                            {
                                                row.Spacing(5);
                                                row.AutoItem().Text($"-").FontColor(sidebarTextColor);
                                                row.RelativeItem().Text($"Skill {i}").FontColor(sidebarTextColor);
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
                                            .Text("Languages")
                                            .FontSize(16)
                                            .FontColor(sidebarTextColor)
                                            );

                                sidebar.Cell().Row(12).ShowOnce().LineHorizontal(1).LineColor(sidebarTextColor);

                                sidebar
                                    .Cell()
                                    .Row(13)
                                    .ShowOnce()
                                    .Column(skills =>
                                    {
                                        foreach (var i in Enumerable.Range(1, 3))
                                        {
                                            skills.Item().PaddingTop(5).Row(row =>
                                            {
                                                row.Spacing(5);
                                                row.AutoItem().Text($"-").FontColor(sidebarTextColor);
                                                row.RelativeItem().Text($"Language {i}").FontColor(sidebarTextColor);
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
                                    .Text("Carreer Objective")
                                    .FontSize(20)
                                    );

                                body.Cell().Row(2).ShowOnce().LineHorizontal(1).LineColor(sidebarBackgroundColor);

                                body.Cell().Row(3).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text("Experience")
                                    .FontSize(20)
                                    );
                                body.Cell().Row(4).ShowOnce().LineHorizontal(1).LineColor(sidebarBackgroundColor);

                                body.Cell().Row(5).Element(container => container
                                    .AlignLeft()
                                    .PaddingBottom(10)
                                    .Text("Education")
                                    .FontSize(20)
                                    );
                                body.Cell().Row(6).ShowOnce().LineHorizontal(1).LineColor(sidebarBackgroundColor);

                            }));
                });
            });
        });
    }
}
