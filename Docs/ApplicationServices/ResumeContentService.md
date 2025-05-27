# Data Preparation Logic (`ResumeContentService.cs`)

The `ResumeContentService` implements [`IResumeDataProvider`](../CoreComponents.md#iresumedataprovider-implemented-by-resumecontentservice) and [`IColorService`](../CoreComponents.md#icolorservice-implemented-by-resumecontentservice). It is responsible for creating [`ResumeData`](../DataModels/ResumeData_And_ContentRequest.md#resumedata-domain-object) objects from various inputs and providing color theme information. It utilizes helpers from [`QuestPdfContentHelpers`](../Helpers/QuestPdfContentHelpers.md).

### 4.1. Method: `ResumeData CreateFromRequest(ContentRequest request)`
*   **Purpose:** Creates `ResumeData` from a [`ContentRequest`](../DataModels/ResumeData_And_ContentRequest.md#contentrequest-dto) DTO, applying defaults and processing inputs.
*   **Input:** `ContentRequest request`. **Output:** `ResumeData`.
*   **Business Rules & Logic Flow:**
    1.  **Direct Mapping:** Fields like `Name`, `Profession`, `Address`, `Phone`, `Email`, `Linkedin`, `Github`, `SkillList`, `LanguageList`, `Summary`, `WorkExperienceList`, `EducationList` are mapped directly from `request` to `resumeData`.
    2.  **Default Text Color:** `resumeData.TextColor` defaults to `Colors.White` (from `QuestPDF.Helpers.Colors` via `QuestPdfContentHelpers`).
    3.  **Default Headers:** `HeaderContact` ("Contact"), `HeaderSkills` ("Skills"), etc., are set to default string values.
    4.  **Avatar Image:** If `request.Base64avatar` is provided, it's converted to an `Image` object (from QuestPDF library using `QuestPdfContentHelpers.Base64ToImage`); otherwise, `resumeData.AvatarImage` is null.
    5.  **Theme Color:** If `request.ThemeColor` is valid (exists in `QuestPdfContentHelpers.ThemeColors`), it's used. Otherwise, `resumeData.PrimaryColor` defaults to `QuestPdfContentHelpers.ThemeColors[Theme.Blue]` (default color from `QuestPDF.Helpers.Colors` via `QuestPdfContentHelpers`).
    6.  **Icons:** Standard icons (Address, Email, Phone, LinkedIn, GitHub) are loaded from `Base64Constants` (internal domain constants) and converted to `Image` objects (QuestPDF).
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        cfrA["Start: CreateFromRequest"] --> cfrB{"request.Base64avatar not empty?"};
        cfrB -- Yes --> cfrC["Convert Base64avatar to Image"];
        cfrC --> cfrD["Set resumeData.AvatarImage"];
        cfrB -- No --> cfrD;
        cfrD --> cfrE{"request.ThemeColor valid?"};
        cfrE -- Yes --> cfrF["Set resumeData.PrimaryColor = request.ThemeColor"];
        cfrF --> cfrG["Map direct fields from request to resumeData"];
        cfrE -- No --> cfrH["Set resumeData.PrimaryColor = Default Blue"];
        cfrH --> cfrG;
        cfrG --> cfrI["Set default TextColor - White"];
        cfrI --> cfrJ["Set default Headers - e.g. Contact, Skills"];
        cfrJ --> cfrK["Load all Icons from Base64Constants"];
        cfrK --> cfrL["Return resumeData"];
        cfrL --> cfrM["End"];
    ```

### 4.2. Method: `ResumeData CreateExample()`
*   **Purpose:** Creates `ResumeData` populated with predefined example content.
*   **Input:** None. **Output:** `ResumeData`.
*   **Business Rules & Logic Flow:**
    1.  **Name Formatting:** `ResumeData.Name` is set to "john doe" then title-cased to "John Doe" (using .NET BCL `TextInfo.ToTitleCase()`).
    2.  **Static Data:** All other textual fields, lists, avatar image (from `Base64Constants`), icons (from `Base64Constants`), `PrimaryColor` (Default Blue from `QuestPdfContentHelpers`), `TextColor` (White from `QuestPDF.Helpers.Colors`), and Header texts are populated with hardcoded example values.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        ceA["Start: CreateExample"] --> ceB["Initialize TextInfo for en-US (NET BCL)"];
        ceB --> ceC["Set Name to john doe, then TitleCase to John Doe"];
        ceC --> ceD["Populate resumeData fields with example strings and lists"];
        ceD --> ceE["Load AvatarImage from Base64Constants"];
        ceE --> ceF["Load all Icons from Base64Constants"];
        ceF --> ceG["Set PrimaryColor = Default Blue"];
        ceG --> ceH["Set TextColor = White"];
        ceH --> ceI["Set default Header texts"];
        ceI --> ceJ["Return resumeData"];
        ceJ --> ceK["End"];
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
        cwcA["Start: CreateWithColor"] --> cwcB["Call CreateExample() to get base resumeData"];
        cwcB --> cwcC{"Input 'color' valid & available?"};
        cwcC -- Yes --> cwcD["Set resumeData.PrimaryColor = input color"];
        cwcD --> cwcE["Return resumeData"];
        cwcC -- No --> cwcF["resumeData.PrimaryColor remains Default Blue from CreateExample()"];
        cwcF --> cwcE;
        cwcE --> cwcG["End"];
    ```

### 4.4. Method: `IEnumerable<string> GetAvailableColors()`
*   **Purpose:** Returns a list of available theme color values.
*   **Input:** None. **Output:** `IEnumerable<string>`.
*   **Business Rules & Logic Flow:** Returns the string values from `QuestPdfContentHelpers.ThemeColors` dictionary.
*   **Mermaid Diagram:**
    ```mermaid
    graph TD
        gacA["Start: GetAvailableColors"] --> gacB["Access QuestPdfContentHelpers.ThemeColors.Values"];
        gacB --> gacC["Return list of color strings"];
        gacC --> gacD["End"];
    ```
```
