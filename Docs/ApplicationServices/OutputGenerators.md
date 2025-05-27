# Output Generation (`PdfGenerator.cs` & `ImageGenerator.cs`)

These services are responsible for converting the structured `IDocument` (created by `QuestPdfDocumentGenerator`) into final output formats.

## 6.1. `PdfGenerator.cs` - Method: `byte[] GeneratePdf(ResumeData resumeData)`
*   **Purpose:** Generates PDF bytes from `ResumeData`.
*   **Logic Flow:**
    1. Calls `IQuestPdfDocumentGenerator.GenerateDocument(resumeData)` to get an `IDocument`.
    2. Calls `document.GeneratePdf()` to get PDF bytes.
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
*   **Purpose:** Generates image bytes (per page) from `ResumeData`.
*   **Logic Flow:**
    1. Calls `IQuestPdfDocumentGenerator.GenerateDocument(resumeData)` to get an `IDocument`.
    2. Calls `document.GenerateImages()` to get a list of image bytes.
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
