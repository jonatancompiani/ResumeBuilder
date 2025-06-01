using QuestPDF.Infrastructure;
using ResumeBuilder.Application;
using ResumeBuilder.Application.Abstractions;
using ResumeBuilder.Domain.Abstractions.Application;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// Register application services
builder.Services.AddScoped<IResumeDataProvider, ResumeContentService>();
builder.Services.AddScoped<IColorService, ResumeContentService>();
builder.Services.AddScoped<IQuestPdfDocumentGenerator, QuestPdfDocumentGenerator>();
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
builder.Services.AddScoped<IImageGenerator, ImageGenerator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(option=> option.AllowAnyOrigin());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
