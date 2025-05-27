# Core Components and Services

*   **`DocController`**: Handles incoming API requests for resume generation and related utilities (e.g., fetching available colors) and routes them to appropriate application services.
*   **`IResumeDataProvider` (implemented by `ResumeContentService`)**: Responsible for creating and preparing the `ResumeData` object. This includes mapping from user requests, generating example data, and applying selected theme colors.
*   **`IColorService` (implemented by `ResumeContentService`)**: Provides information about available theme colors that can be used in resumes.
*   **`IQuestPdfDocumentGenerator` (implemented by `QuestPdfDocumentGenerator`)**: Defines the detailed visual structure and content layout of the resume document using the QuestPDF library.
*   **`IPdfGenerator` (implemented by `PdfGenerator`)**: Generates PDF byte arrays from a `ResumeData` object.
*   **`IImageGenerator` (implemented by `ImageGenerator`)**: Generates image byte arrays (one per page) from a `ResumeData` object.
*   **`QuestPdfContentHelpers`**: A static utility class providing a dictionary of theme colors and a helper method for Base64 to `Image` object conversion.
*   **`ResumeData`**: A domain object (POCO) that holds all necessary data required to render a resume (e.g., personal info, skills, experience, colors, icons).
*   **`ContentRequest`**: A Data Transfer Object (DTO) used for receiving user-provided resume data through API requests.
