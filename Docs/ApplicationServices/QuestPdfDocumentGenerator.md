# Document Generation Logic (`QuestPdfDocumentGenerator.cs`)

The `QuestPdfDocumentGenerator` class implements `IQuestPdfDocumentGenerator` and is responsible for defining the detailed visual structure and content layout of the resume document using the QuestPDF library. Its main method is `GenerateDocument(ResumeData resumeData)`.

## 5.1. Overall Document Structure
*   **Rule:** A single page document.
*   **Layout Rule:** The page uses a main table with two columns:
    *   Column 1 (Sidebar): Relative width of 1.
    *   Column 2 (Main Content): Relative width of 2.

## 5.2. Sidebar (Left Column)
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
            avA["Start Avatar"] --> avB{"resumeData.AvatarImage is not null?"};
            avB -- Yes --> avC["Display AvatarImage"];
            avC --> avD["Draw Circular Border - Color PrimaryColor"];
            avD --> avE["End Avatar"];
            avB -- No --> avE;
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
            ciA["Start Contact Item - e.g. Address"] --> ciB{"resumeData.Address not empty?"};
            ciB -- Yes --> ciC["Display AddressIcon"];
            ciC --> ciD["Display Address Text"];
            ciD --> ciE["Next Contact Item or End"];
            ciB -- No --> ciE;
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
            slA["Start Skills"] --> slB{"resumeData.SkillList is null?"};
            slB -- Yes --> slC["Use Empty List"];
            slC --> slD;
            slB -- No --> slD["Use resumeData.SkillList"];
            slD --> slE["For each skill in list"];
            slE -- Loop Item --> slF["Display Bullet"];
            slF --> slG["Display Skill Text"];
            slG --> slE;
            slE -- Loop End --> slH["End Skills"];
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
            llA["Start Languages"] --> llB{"resumeData.LanguageList is null?"};
            llB -- Yes --> llC["Use Empty List"];
            llC --> llD;
            llB -- No --> llD["Use resumeData.LanguageList"];
            llD --> llE["For each language in list"];
            llE -- Loop Item --> llF["Display Language Name with Bullet"];
            llF --> llG["Display Language Level - indented"];
            llG --> llE;
            llE -- Loop End --> llH["End Languages"];
        ```

## 5.3. Main Content (Right Column)
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
        *   Display `wex.JobTitle` - Bold.
        *   Display `wex.Company` - Light.
        *   Display `wex.Description`.
    *   **Mermaid (Work Experience Logic):**
        ```mermaid
        graph TD
            weA["Start Experience"] --> weB{"resumeData.WorkExperienceList is null?"};
            weB -- Yes --> weC["Use Empty List"];
            weC --> weD;
            weB -- No --> weD["Use resumeData.WorkExperienceList"];
            weD --> weE["For each wex in list"];
            weE -- Loop Item --> weF["Display Year"];
            weF --> weG["Display JobTitle - Bold"];
            weG --> weH["Display Company - Light"];
            weH --> weI["Display Description"];
            weI --> weE;
            weE -- Loop End --> weJ["End Experience"];
        ```

    #### 5.3.3. Education Section
    *   **Content Rule:** Displays `resumeData.HeaderEducation`.
    *   Followed by a horizontal line (Color: `resumeData.PrimaryColor`).
    *   **Looping Rule (EducationList):** For each `edu` in `resumeData.EducationList` (or empty list if null):
        *   Display `edu.Year` (Thin, Font Size 9).
        *   Display `edu.Title` - Bold.
        *   Display `edu.Institution` - Light.
        *   Display `edu.Description`.
    *   **Mermaid (Education Logic):**
        ```mermaid
        graph TD
            edA["Start Education"] --> edB{"resumeData.EducationList is null?"};
            edB -- Yes --> edC["Use Empty List"];
            edC --> edD;
            edB -- No --> edD["Use resumeData.EducationList"];
            edD --> edE["For each edu in list"];
            edE -- Loop Item --> edF["Display Year"];
            edF --> edG["Display Title - Bold"];
            edG --> edH["Display Institution - Light"];
            edH --> edI["Display Description"];
            edI --> edE;
            edE -- Loop End --> edJ["End Education"];
        ```
```
