# Helper Utilities (`QuestPdfContentHelpers.cs`)

This static utility class provides common functionalities used across the application, particularly by the `ResumeContentService`.

*   **`ThemeColors` (Public Static Dictionary):**
    *   **Purpose:** Stores predefined theme names (represented by the `Theme` enum) and their corresponding QuestPDF color string values (e.g., `Colors.Blue.Darken4`).
    *   **Usage:** Used by `ResumeContentService` for validating user-selected theme colors and for providing a list of available colors via the `IColorService` interface.

*   **`Base64ToImage(string base64String)` (Public Static Method):**
    *   **Purpose:** Converts a Base64 encoded string into a QuestPDF `Image` object.
    *   **Input:** `string base64String` - The Base64 encoded image data.
    *   **Output:** `QuestPDF.Infrastructure.Image` - The decoded image object.
    *   **Usage:** Used by `ResumeContentService` to process avatar images and to load predefined icons from `Base64Constants`.
    *   **Logic:**
        1. Converts the input Base64 string to a byte array (`Convert.FromBase64String`).
        2. Creates a `MemoryStream` from the byte array.
        3. Uses `Image.FromStream(ms)` to create the QuestPDF `Image` object.
```
