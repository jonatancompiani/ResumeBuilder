# ResumeBuilder Application: Overview

This document describes the business logic and process flows of the ResumeBuilder application. It focuses on how resume data is handled, processed from API requests or example data, and how PDF and image outputs of resumes are generated. The documentation includes textual descriptions and Mermaid syntax diagrams for key processes.

## Document Structure

This documentation is organized into the following main sections:

*   [Core Components and Services](./CoreComponents.md): Describes the main architectural pieces of the application.
*   [API Endpoint Flows](./API/DocController_Endpoints.md): Details the API provided by the `DocController`.
*   Application Services:
    *   [Data Preparation (`ResumeContentService`)](./ApplicationServices/ResumeContentService.md): Explains how resume data is created and processed.
    *   [Document Generation (`QuestPdfDocumentGenerator`)](./ApplicationServices/QuestPdfDocumentGenerator.md): Details the PDF structure generation.
    *   [Output Generation (`PdfGenerator` & `ImageGenerator`)](./ApplicationServices/OutputGenerators.md): Covers how final PDF/image files are produced.
*   [Data Models](./DataModels/ResumeData_And_ContentRequest.md): Information on key data structures.
*   [Helper Utilities](./Helpers/QuestPdfContentHelpers.md): Describes helper classes.
```
