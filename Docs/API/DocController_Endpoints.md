# API Endpoint Flows (`DocController.cs`)

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
```
