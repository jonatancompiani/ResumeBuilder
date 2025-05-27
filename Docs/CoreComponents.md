# Core Components and Services

*   **[`DocController`](./API/DocController_Endpoints.md)**: Handles incoming API requests for resume generation and related utilities (e.g., fetching available colors) and routes them to appropriate application services.
*   **`IResumeDataProvider` (implemented by [`ResumeContentService`](./ApplicationServices/ResumeContentService.md))**: Responsible for creating and preparing the `ResumeData` object. This includes mapping from user requests, generating example data, and applying selected theme colors.
*   **`IColorService` (implemented by [`ResumeContentService`](./ApplicationServices/ResumeContentService.md#44-method-ienumerablestring-getavailablecolors))**: Provides information about available theme colors that can be used in resumes.
*   **`IQuestPdfDocumentGenerator` (implemented by [`QuestPdfDocumentGenerator`](./ApplicationServices/QuestPdfDocumentGenerator.md))**: Defines the detailed visual structure and content layout of the resume document (using the QuestPDF library).
*   **`IPdfGenerator` (implemented by [`PdfGenerator`](./ApplicationServices/OutputGenerators.md#61-pdfgeneratorcs---method-byte-generatepdfresumedata-resumedata))**: Generates PDF byte arrays from a `ResumeData` object.
*   **`IImageGenerator` (implemented by [`ImageGenerator`](./ApplicationServices/OutputGenerators.md#62-imagegeneratorcs---method-ienumerablebyte-generateimagesresumedata-resumedata))**: Generates image byte arrays (one per page) from a `ResumeData` object.
*   **[`QuestPdfContentHelpers`](./Helpers/QuestPdfContentHelpers.md)**: A static utility class providing a dictionary of theme colors and a helper method for Base64 to `Image` object conversion.
*   **[`ResumeData`](./DataModels/ResumeData_And_ContentRequest.md#resumedata-domain-object)**: A domain object (POCO) that holds all necessary data required to render a resume (e.g., personal info, skills, experience, colors, icons).
*   **[`ContentRequest`](./DataModels/ResumeData_And_ContentRequest.md#contentrequest-dto)**: A Data Transfer Object (DTO) used for receiving user-provided resume data through API requests.
```
