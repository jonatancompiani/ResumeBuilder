using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;

namespace ResumeBuilder.Application
{
    public interface IQuestPdfDocumentGenerator
    {
        IDocument GenerateDocument(ResumeData resumeData);
    }
}
