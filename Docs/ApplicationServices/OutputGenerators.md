# Output Generation (`PdfGenerator.cs` & `ImageGenerator.cs`)

These services are responsible for converting the structured `IDocument` (created by [`QuestPdfDocumentGenerator`](./QuestPdfDocumentGenerator.md)) into final output formats. They both depend on [`IQuestPdfDocumentGenerator`](../CoreComponents.md#iquestpdfdocumentgenerator-implemented-by-questpdfdocumentgenerator).

## 6.1. `PdfGenerator.cs` - Method: `byte[] GeneratePdf(ResumeData resumeData)`
*   **Purpose:** Generates PDF bytes from [`ResumeData`](../DataModels/ResumeData_And_ContentRequest.md#resumedata-domain-object).
*   **Logic Flow:**
    1. Calls `IQuestPdfDocumentGenerator.GenerateDocument(resumeData)` to get an `IDocument` (QuestPDF interface).
    2. Calls `document.GeneratePdf()` (QuestPDF method) to get PDF bytes.
    3. Returns bytes.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Caller
        participant PdfGenerator
        participant IQuestPdfDocumentGenerator
        participant IDocument

        Caller->>+PdfGenerator: GeneratePdf(resumeData)
        PdfGenerator->>+IQuestPdfDocumentGenerator: GenerateDocument(resumeData)
        IQuestPdfDocumentGenerator-->>-PdfGenerator: document (IDocument)
        PdfGenerator->>+IDocument: GeneratePdf()
        IDocument-->>-PdfGenerator: pdfBytes
        PdfGenerator-->>-Caller: pdfBytes
    ```

## 6.2. `ImageGenerator.cs` - Method: `IEnumerable<byte[]> GenerateImages(ResumeData resumeData)`
*   **Purpose:** Generates image bytes (per page) from [`ResumeData`](../DataModels/ResumeData_And_ContentRequest.md#resumedata-domain-object).
*   **Logic Flow:**
    1. Calls `IQuestPdfDocumentGenerator.GenerateDocument(resumeData)` to get an `IDocument` (QuestPDF interface).
    2. Calls `document.GenerateImages()` (QuestPDF method) to get a list of image bytes.
    3. Returns list of bytes.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Caller
        participant ImageGenerator
        participant IQuestPdfDocumentGenerator
        participant IDocument

        Caller->>+ImageGenerator: GenerateImages(resumeData)
        ImageGenerator->>+IQuestPdfDocumentGenerator: GenerateDocument(resumeData)
        IQuestPdfDocumentGenerator-->>-ImageGenerator: document (IDocument)
        ImageGenerator->>+IDocument: GenerateImages()
        IDocument-->>-ImageGenerator: imageBytesList
        ImageGenerator-->>-Caller: imageBytesList
    ```
```
