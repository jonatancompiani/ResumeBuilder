using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

using ResumeBuilder.Application; // Added for service registration
// QuestPDF.Infrastructure is already present

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.

// Register application services
builder.Services.AddScoped<IResumeDataProvider, ResumeContentService>();
builder.Services.AddScoped<IColorService, ResumeContentService>();
builder.Services.AddScoped<IQuestPdfDocumentGenerator, QuestPdfDocumentGenerator>();
builder.Services.AddScoped<IPdfGenerator, PdfGenerator>();
builder.Services.AddScoped<IImageGenerator, ImageGenerator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
