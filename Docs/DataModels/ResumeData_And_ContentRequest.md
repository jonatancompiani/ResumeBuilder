# Data Models

This section describes the primary data structures used for handling resume information.

## `ResumeData` (Domain Object)

*   **Purpose:** A domain object (POCO - Plain Old CLR Object) that holds all necessary data required to render a complete resume. This object is populated by the [`ResumeContentService`](../ApplicationServices/ResumeContentService.md) and consumed by the [`QuestPdfDocumentGenerator`](../ApplicationServices/QuestPdfDocumentGenerator.md).
*   **Key Attributes (examples):**
    *   Personal Information: `Name`, `Profession`, `Address`, `Phone`, `Email`, `Linkedin`, `Github`
    *   Content Sections: `Summary`, `SkillList`, `LanguageList`, `WorkExperienceList`, `EducationList`
    *   Styling & Assets: `PrimaryColor`, `TextColor`, `AvatarImage` (QuestPDF `Image` object), various `Icon` images (QuestPDF `Image` objects e.g., `AddressIcon`, `EmailIcon`), Header texts (e.g., `HeaderContact`, `HeaderSkills`).
*   **Note:** This object is the comprehensive internal representation of a resume's content and styling directives before it's rendered.

## `ContentRequest` (DTO)

*   **Purpose:** A Data Transfer Object (DTO) used for receiving user-provided resume data through API requests (specifically for endpoints in [`DocController`](../API/DocController_Endpoints.md) like `/Doc/Preview` and `/Doc/Download`).
*   **Key Attributes:** Contains fields corresponding to the data a user can provide for their resume, such as `Name`, `Base64avatar`, `ThemeColor`, `Profession`, contact details, summary, skills, languages, work experiences, and education details.
*   **Note:** This DTO serves as the input contract for user-customized resume generation. The [`ResumeContentService`](../ApplicationServices/ResumeContentService.md) maps data from `ContentRequest` to the internal `ResumeData` model, applying defaults and business logic where necessary.
```
