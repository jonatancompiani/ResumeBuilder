# ResumeBuilder Application: Business Rules and Process Flows

## 1. Introduction

This document describes the business logic and process flows of the ResumeBuilder application. It focuses on how resume data is handled, processed from API requests or example data, and how PDF and image outputs of resumes are generated. The documentation includes textual descriptions and Mermaid syntax diagrams for key processes.

## 2. Core Components and Services

*   **`DocController`**: Handles incoming API requests for resume generation and related utilities (e.g., fetching available colors) and routes them to appropriate application services.
*   **`IResumeDataProvider` (implemented by `ResumeContentService`)**: Responsible for creating and preparing the `ResumeData` object. This includes mapping from user requests, generating example data, and applying selected theme colors.
*   **`IColorService` (implemented by `ResumeContentService`)**: Provides information about available theme colors that can be used in resumes.
*   **`IQuestPdfDocumentGenerator` (implemented by `QuestPdfDocumentGenerator`)**: Defines the detailed visual structure and content layout of the resume document using the QuestPDF library.
*   **`IPdfGenerator` (implemented by `PdfGenerator`)**: Generates PDF byte arrays from a `ResumeData` object.
*   **`IImageGenerator` (implemented by `ImageGenerator`)**: Generates image byte arrays (one per page) from a `ResumeData` object.
*   **`QuestPdfContentHelpers`**: A static utility class providing a dictionary of theme colors and a helper method for Base64 to `Image` object conversion.
*   **`ResumeData`**: A domain object (POCO) that holds all necessary data required to render a resume (e.g., personal info, skills, experience, colors, icons).
*   **`ContentRequest`**: A Data Transfer Object (DTO) used for receiving user-provided resume data through API requests.

## 3. API Endpoint Flows (`DocController.cs`)

The `DocController` exposes several endpoints for generating resumes and retrieving related information.

**Overall Controller Business Rules:**
*   **Dependency Injection:** The controller utilizes `IResumeDataProvider`, `IPdfGenerator`, `IImageGenerator`, and `IColorService`, which are injected via its constructor.
*   **Error Handling:** Endpoints performing operations that might fail are wrapped in `try-catch` blocks.
    *   **Rule:** If an exception occurs, the API returns an HTTP 400 BadRequest.
    *   **Rule:** The error message in the BadRequest includes the exception message, inner exception message (if any), and stack trace. (Note: Exposing stack traces in production responses is generally discouraged).
*   **Route Prefix:** All routes are prefixed with `/Doc`.

### 3.1. `GET /Doc/ExampleDownload`
*   **Purpose:** Generates and allows downloading of a PDF resume using predefined example data, customized with a user-specified color.
*   **Parameters:** `color` (string, from query): The desired primary theme color for the resume.
*   **Flow Description:** The controller receives the request, calls `IResumeDataProvider.CreateWithColor(color)` to get `ResumeData`, then passes this to `IPdfGenerator.GeneratePdf(resumeData)` to get the PDF bytes, which are returned in an HTTP 200 OK response.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Client
        participant DocController
        participant IResumeDataProvider
        participant IPdfGenerator

        Client->>+DocController: GET /Doc/ExampleDownload?color={color}
        DocController->>+IResumeDataProvider: CreateWithColor(color)
        IResumeDataProvider-->>-DocController: resumeData
        DocController->>+IPdfGenerator: GeneratePdf(resumeData)
        IPdfGenerator-->>-DocController: pdfBytes
        DocController-->>-Client: HTTP 200 OK (pdfBytes)
    ```
*   **Business Rules:** Output is PDF. Content is example data, themed with the input color.

### 3.2. `GET /Doc/Colors`
*   **Purpose:** Retrieves a list of available theme color names/values.
*   **Parameters:** None.
*   **Flow Description:** The controller calls `IColorService.GetAvailableColors()` and returns the list in an HTTP 200 OK response.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Client
        participant DocController
        participant IColorService

        Client->>+DocController: GET /Doc/Colors
        DocController->>+IColorService: GetAvailableColors()
        IColorService-->>-DocController: colorList
        DocController-->>-Client: HTTP 200 OK (colorList)
    ```
*   **Business Rules:** Color list is predefined.

### 3.3. `GET /Doc/ExamplePreview`
*   **Purpose:** Generates and returns image(s) of a resume using example data, themed with a specified color.
*   **Parameters:** `color` (string, from query): The desired primary theme color.
*   **Flow Description:** The controller calls `IResumeDataProvider.CreateWithColor(color)` for `ResumeData`, then `IImageGenerator.GenerateImages(resumeData)` for image bytes, returned in an HTTP 200 OK response.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Client
        participant DocController
        participant IResumeDataProvider
        participant IImageGenerator

        Client->>+DocController: GET /Doc/ExamplePreview?color={color}
        DocController->>+IResumeDataProvider: CreateWithColor(color)
        IResumeDataProvider-->>-DocController: resumeData
        DocController->>+IImageGenerator: GenerateImages(resumeData)
        IImageGenerator-->>-DocController: imageBytesList
        DocController-->>-Client: HTTP 200 OK (imageBytesList)
    ```
*   **Business Rules:** Output is image(s). Content is example data, themed with input color.

### 3.4. `POST /Doc/Preview`
*   **Purpose:** Generates and returns image(s) of a resume based on user-provided data in the request body.
*   **Parameters:** `content` (`ContentRequest` object, from request body).
*   **Flow Description:** The controller calls `IResumeDataProvider.CreateFromRequest(content)` for `ResumeData`, then `IImageGenerator.GenerateImages(resumeData)` for image bytes, returned in an HTTP 200 OK response.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Client
        participant DocController
        participant IResumeDataProvider
        participant IImageGenerator

        Client->>+DocController: POST /Doc/Preview (ContentRequest)
        DocController->>+IResumeDataProvider: CreateFromRequest(content)
        IResumeDataProvider-->>-DocController: resumeData
        DocController->>+IImageGenerator: GenerateImages(resumeData)
        IImageGenerator-->>-DocController: imageBytesList
        DocController-->>-Client: HTTP 200 OK (imageBytesList)
    ```
*   **Business Rules:** Output is image(s). Content is based on user input.

### 3.5. `POST /Doc/Download`
*   **Purpose:** Generates and allows downloading of a PDF resume based on user-provided data.
*   **Parameters:** `content` (`ContentRequest` object, from request body).
*   **Flow Description:** The controller calls `IResumeDataProvider.CreateFromRequest(content)` for `ResumeData`, then `IPdfGenerator.GeneratePdf(resumeData)` for PDF bytes, returned in an HTTP 200 OK response.
*   **Mermaid Diagram:**
    ```mermaid
    sequenceDiagram
        participant Client
        participant DocController
        participant IResumeDataProvider
        participant IPdfGenerator

        Client->>+DocController: POST /Doc/Download (ContentRequest)
        DocController->>+IResumeDataProvider: CreateFromRequest(content)
        IResumeDataProvider-->>-DocController: resumeData
        DocController->>+IPdfGenerator: GeneratePdf(resumeData)
        IPdfGenerator-->>-DocController: pdfBytes
        DocController-->>-Client: HTTP 200 OK (pdfBytes)
    ```
*   **Business Rules:** Output is PDF. Content is based on user input.

## 4. Data Preparation Logic (`ResumeContentService.cs`)

### 4.1. Method: `ResumeData CreateFromRequest(ContentRequest request)`
*   **Purpose:** Creates `ResumeData` from a `ContentRequest` DTO, applying defaults and processing inputs.
*   **Input:** `ContentRequest request`. **Output:** `ResumeData`.
*   **Business Rules & Logic Flow:**
    1.  **Direct Mapping:** Fields like `Name`, `Profession`, `Address`, `Phone`, `Email`, `Linkedin`, `Github`, `SkillList`, `LanguageList`, `Summary`, `WorkExperienceList`, `EducationList` are mapped directly from `request` to `resumeData`.
    2.  **Default Text Color:** `resumeData.TextColor` defaults to `Colors.White`.
    3.  **Default Headers:** `HeaderContact` ("Contact"), `HeaderSkills` ("Skills"), etc., are set to default string values.
    4.  **Avatar Image:** If `request.Base64avatar` is provided, it's converted to an `Image` object; otherwise, `resumeData.AvatarImage` is null.
    5.  **Theme Color:** If `request.ThemeColor` is valid (exists in `QuestPdfContentHelpers.ThemeColors`), it's used. Otherwise, `resumeData.PrimaryColor` defaults to `QuestPdfContentHelpers.ThemeColors[Theme.Blue]`.
    6.  **Icons:** Standard icons (Address, Email, Phone, LinkedIn, GitHub) are loaded from `Base64Constants` and converted to `Image` objects.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        A[Start: CreateFromRequest] --> B{request.Base64avatar not empty?};
        B -- Yes --> C[Convert Base64avatar to Image];
        C --> D[Set resumeData.AvatarImage];
        B -- No --> D;
        D --> E{request.ThemeColor valid?};
        E -- Yes --> F[Set resumeData.PrimaryColor = request.ThemeColor];
        F --> G[Map direct fields from request to resumeData];
        E -- No --> H[Set resumeData.PrimaryColor = Default Blue];
        H --> G;
        G --> I[Set default TextColor (White)];
        I --> J[Set default Headers (e.g., "Contact", "Skills")];
        J --> K[Load all Icons from Base64Constants];
        K --> L[Return resumeData];
        L --> M[End];
    ```

### 4.2. Method: `ResumeData CreateExample()`
*   **Purpose:** Creates `ResumeData` populated with predefined example content.
*   **Input:** None. **Output:** `ResumeData`.
*   **Business Rules & Logic Flow:**
    1.  **Name Formatting:** `ResumeData.Name` is set to "john doe" then title-cased to "John Doe".
    2.  **Static Data:** All other textual fields, lists (Skills, Languages, Experience, Education), avatar image (from `Base64Constants`), icons (from `Base64Constants`), `PrimaryColor` (Default Blue), `TextColor` (White), and Header texts are populated with hardcoded example values.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        A[Start: CreateExample] --> B[Initialize TextInfo for "en-US"];
        B --> C[Set Name to "john doe", then TitleCase to "John Doe"];
        C --> D[Populate resumeData fields with example strings and lists];
        D --> E[Load AvatarImage from Base64Constants];
        E --> F[Load all Icons from Base64Constants];
        F --> G[Set PrimaryColor = Default Blue];
        G --> H[Set TextColor = White];
        H --> I[Set default Header texts];
        I --> J[Return resumeData];
        J --> K[End];
    ```

### 4.3. Method: `ResumeData CreateWithColor(string color)`
*   **Purpose:** Creates `ResumeData` using example data but overrides the primary theme color.
*   **Input:** `string color`. **Output:** `ResumeData`.
*   **Business Rules & Logic Flow:**
    1.  Calls `CreateExample()` to get base `ResumeData`.
    2.  If the input `color` is valid (exists in `QuestPdfContentHelpers.ThemeColors`), `resumeData.PrimaryColor` is updated. Otherwise, it remains the default blue from `CreateExample()`.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        A[Start: CreateWithColor] --> B[Call CreateExample() to get base resumeData];
        B --> C{Input 'color' valid & available?};
        C -- Yes --> D[Set resumeData.PrimaryColor = input 'color'];
        D --> E[Return resumeData];
        C -- No --> F[resumeData.PrimaryColor remains Default Blue from CreateExample()];
        F --> E;
        E --> G[End];
    ```

### 4.4. Method: `IEnumerable<string> GetAvailableColors()`
*   **Purpose:** Returns a list of available theme color values.
*   **Input:** None. **Output:** `IEnumerable<string>`.
*   **Business Rules & Logic Flow:** Returns the string values from `QuestPdfContentHelpers.ThemeColors` dictionary.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        A[Start: GetAvailableColors] --> B[Access QuestPdfContentHelpers.ThemeColors.Values];
        B --> C[Return list of color strings];
        C --> D[End];
    ```

## 5. Document Generation Logic (`QuestPdfDocumentGenerator.cs`)

The `GenerateDocument` method defines the resume's layout.

### 5.1. Overall Document Structure
*   **Rule:** A single page document.
*   **Layout Rule:** The page uses a main table with two columns:
    *   Column 1 (Sidebar): Relative width of 1.
    *   Column 2 (Main Content): Relative width of 2.

### 5.2. Sidebar (Left Column)
*   **Styling Rules:**
    *   Border: 1 unit, color `resumeData.PrimaryColor`.
    *   Background: `resumeData.PrimaryColor`.
    *   Extends vertically.
    *   Padding: 15 units.
*   Contains an inner table for its content elements.

    #### 5.2.1. Avatar Image Section
    *   **Display Rule:** Centered, max height 120.
    *   **Conditional Rule (Avatar):** If `resumeData.AvatarImage` exists:
        *   Displays `resumeData.AvatarImage` (fit height).
        *   Draws a circular stroke border around it using `resumeData.PrimaryColor`.
    *   **Mermaid (Avatar Logic):**
        ```mermaid
        graph TD
            A_Avatar[Start Avatar] --> B_HasAvatar{resumeData.AvatarImage is not null?};
            B_HasAvatar -- Yes --> C_ShowImage[Display AvatarImage];
            C_ShowImage --> D_DrawBorder[Draw Circular Border (Color: PrimaryColor)];
            D_DrawBorder --> E_EndAvatar[End Avatar];
            B_HasAvatar -- No --> E_EndAvatar;
        ```

    #### 5.2.2. Name Section
    *   **Content Rule:** Displays `resumeData.Name`.
    *   **Styling Rules:** Centered, Font Size 24, Color `resumeData.TextColor`.

    #### 5.2.3. Profession Section
    *   **Content Rule:** Displays `resumeData.Profession` (if not null/empty).
    *   **Styling Rules:** Centered, Font Size 12, Color `resumeData.TextColor`.

    #### 5.2.4. Contact Header Section
    *   **Content Rule:** Displays `resumeData.HeaderContact`.
    *   **Styling Rules:** Padding Top 20, Left Align, Padding Bottom 10, Font Size 12, Color `resumeData.TextColor`.
    *   Followed by a horizontal line (Color: `resumeData.TextColor`).

    #### 5.2.5. Contact Details Section
    *   **Layout:** Inner table (10mm icon column, relative text column).
    *   **Conditional Rules (for Address, Phone, Email, LinkedIn, GitHub):**
        *   If the respective `resumeData` field (e.g., `resumeData.Address`) is not null/empty:
            *   Display corresponding icon (`resumeData.AddressIcon`) in column 1.
            *   Display text in column 2 (Font Size 9, Color `resumeData.TextColor`).
    *   **Mermaid (Generic Contact Item Logic):**
        ```mermaid
        graph TD
            F_ContactItem[Start Contact Item (e.g. Address)] --> G_HasData{resumeData.Address not empty?};
            G_HasData -- Yes --> H_ShowIcon[Display AddressIcon];
            H_ShowIcon --> I_ShowText[Display Address Text];
            I_ShowText --> J_NextContact[Next Contact Item or End];
            G_HasData -- No --> J_NextContact;
        ```

    #### 5.2.6. Skills Header Section
    *   **Content Rule:** Displays `resumeData.HeaderSkills`.
    *   **Styling:** (Similar to Contact Header).
    *   Followed by a horizontal line (Color: `resumeData.TextColor`).

    #### 5.2.7. Skills List Section
    *   **Looping Rule:** For each `skill` in `resumeData.SkillList` (or empty list if null):
        *   Display a bullet point (`•`).
        *   Display the `skill` text.
        *   **Styling:** Font Size 10, Color `resumeData.TextColor`.
    *   **Mermaid (Skills List Logic):**
        ```mermaid
        graph TD
            K_Skills[Start Skills] --> L_NullCheck{resumeData.SkillList is null?};
            L_NullCheck -- Yes --> M_EmptyList[Use Empty List];
            M_EmptyList --> N_Loop;
            L_NullCheck -- No --> N_Loop[Use resumeData.SkillList];
            N_Loop --> O_ForEachSkill[For each skill];
            O_ForEachSkill -- Loop Item --> P_ShowBullet[Display Bullet];
            P_ShowBullet --> Q_ShowSkill[Display Skill Text];
            Q_ShowSkill --> O_ForEachSkill;
            O_ForEachSkill -- Loop End --> S_EndSkills[End Skills];
        ```

    #### 5.2.8. Languages Header Section
    *   **Content Rule:** Displays `resumeData.HeaderLanguages`.
    *   **Styling:** (Similar to Contact Header).
    *   Followed by a horizontal line (Color: `resumeData.TextColor`).

    #### 5.2.9. Languages List Section
    *   **Looping Rule:** For each `language` in `resumeData.LanguageList` (or empty list if null):
        *   Display `• {language.Name}`.
        *   Display `Level: {language.Level}` (indented).
        *   **Styling:** Name (Font Size 10), Level (Font Size 9, Light), Color `resumeData.TextColor`.
    *   **Mermaid (Languages List Logic):**
        ```mermaid
        graph TD
            T_Lang[Start Languages] --> U_LangNull{resumeData.LanguageList is null?};
            U_LangNull -- Yes --> V_LangEmpty[Use Empty List];
            V_LangEmpty --> W_LangLoop;
            U_LangNull -- No --> W_LangLoop[Use resumeData.LanguageList];
            W_LangLoop --> X_ForEachLang[For each language];
            X_ForEachLang -- Loop Item --> Y_ShowName[Display Language Name with Bullet];
            Y_ShowName --> Z_ShowLevel[Display Language Level (indented)];
            Z_ShowLevel --> X_ForEachLang;
            X_ForEachLang -- Loop End --> AB_EndLang[End Languages];
        ```

### 5.3. Main Content (Right Column)
*   **Styling Rules:** Padding 15 units. Text generally `Colors.Black`.
*   Contains an inner table for its content elements.

    #### 5.3.1. Summary Section
    *   **Content Rule:** Displays `resumeData.HeaderSummary`.
    *   Followed by a horizontal line (Color: `resumeData.PrimaryColor`).
    *   **Content Rule:** Displays `resumeData.Summary` (if not null/empty).
    *   **Styling:** Font Size 12.

    #### 5.3.2. Experience Section
    *   **Content Rule:** Displays `resumeData.HeaderExperience`.
    *   Followed by a horizontal line (Color: `resumeData.PrimaryColor`).
    *   **Looping Rule (WorkExperienceList):** For each `wex` in `resumeData.WorkExperienceList` (or empty list if null):
        *   Display `wex.Year` (Thin, Font Size 9).
        *   Display `wex.JobTitle` (Bold).
        *   Display `wex.Company` (Light).
        *   Display `wex.Description`.
    *   **Mermaid (Work Experience Logic):**
        ```mermaid
        graph TD
            AC_Exp[Start Experience] --> AD_ExpNull{resumeData.WorkExperienceList is null?};
            AD_ExpNull -- Yes --> AE_ExpEmpty[Use Empty List];
            AE_ExpEmpty --> AF_ExpLoop;
            AD_ExpNull -- No --> AF_ExpLoop[Use resumeData.WorkExperienceList];
            AF_ExpLoop --> AG_ForEachWex[For each wex];
            AG_ForEachWex -- Loop Item --> AH_WexYear[Display Year];
            AH_WexYear --> AI_WexTitle[Display JobTitle (Bold)];
            AI_WexTitle --> AJ_WexCo[Display Company (Light)];
            AJ_WexCo --> AK_WexDesc[Display Description];
            AK_WexDesc --> AG_ForEachWex;
            AG_ForEachWex -- Loop End --> AM_EndExp[End Experience];
        ```

    #### 5.3.3. Education Section
    *   **Content Rule:** Displays `resumeData.HeaderEducation`.
    *   Followed by a horizontal line (Color: `resumeData.PrimaryColor`).
    *   **Looping Rule (EducationList):** For each `edu` in `resumeData.EducationList` (or empty list if null):
        *   Display `edu.Year` (Thin, Font Size 9).
        *   Display `edu.Title` (Bold).
        *   Display `edu.Institution` (Light).
        *   Display `edu.Description`.
    *   **Mermaid (Education Logic):**
        ```mermaid
        graph TD
            AN_Edu[Start Education] --> AO_EduNull{resumeData.EducationList is null?};
            AO_EduNull -- Yes --> AP_EduEmpty[Use Empty List];
            AP_EduEmpty --> AQ_EduLoop;
            AO_EduNull -- No --> AQ_EduLoop[Use resumeData.EducationList];
            AQ_EduLoop --> AR_ForEachEdu[For each edu];
            AR_ForEachEdu -- Loop Item --> AS_EduYear[Display Year];
            AS_EduYear --> AT_EduTitle[Display Title (Bold)];
            AT_EduTitle --> AU_EduInst[Display Institution (Light)];
            AU_EduInst --> AV_EduDesc[Display Description];
            AV_EduDesc --> AR_ForEachEdu;
            AR_ForEachEdu -- Loop End --> AW_EndEdu[End Education];
        ```

## 6. Output Generation (`PdfGenerator.cs` & `ImageGenerator.cs`)

### 6.1. `PdfGenerator.cs` - Method: `byte[] GeneratePdf(ResumeData resumeData)`
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

### 6.2. `ImageGenerator.cs` - Method: `IEnumerable<byte[]> GenerateImages(ResumeData resumeData)`
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

## 7. Helper Utilities (`QuestPdfContentHelpers.cs`)
*   **`ThemeColors` (Dictionary):** Stores predefined theme names (enum `Theme`) and their corresponding QuestPDF color string values (e.g., `Colors.Blue.Darken4`). Used by `ResumeContentService` for color validation and selection.
*   **`Base64ToImage(string base64String)` (static method):** Converts a Base64 encoded string into a QuestPDF `Image` object. Used by `ResumeContentService` for avatar and icon images.

```
