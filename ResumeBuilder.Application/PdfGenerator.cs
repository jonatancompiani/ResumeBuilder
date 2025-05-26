using QuestPDF.Infrastructure;
using ResumeBuilder.Domain;

namespace ResumeBuilder.Application
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly IQuestPdfDocumentGenerator _documentGenerator;

        public PdfGenerator(IQuestPdfDocumentGenerator documentGenerator)
        {
            _documentGenerator = documentGenerator;
        }

        public byte[] GeneratePdf(ResumeData resumeData)
        {
            IDocument document = _documentGenerator.GenerateDocument(resumeData);
            return document.GeneratePdf();
        }
    }
}
