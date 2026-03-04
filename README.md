# Vertex University

A Blazor web application for managing university events, feedback, and student information. This project was formerly known as "BookAPP" and has been rebranded to "Vertex University".

## Features
- **Event Management**: View and manage upcoming university events.
- **Feedback System**: Collect and manage student feedback.
- **Manager Dashboard**: Manager dashboard to oversee university operations.

## Technology Stack
- **Framework**: Blazor
- **Language**: C# / HTML / CSS
- **.NET Version**: .NET 9.0

## Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022, Visual Studio Code, or any preferred IDE.

### Running the Application Locally

1. Open a terminal and navigate to the project directory:
   ```bash
   cd "Vertex University\Vertex University\VertexUniversity"
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   # or for hot reload:
   dotnet watch run
   ```

## Project Directory Structure
- `Pages/`: Contains the main Blazor UI components (e.g., `Home.razor`, `Events.razor`, `FeedbackPage.razor`).
- `wwwroot/`: Contains static web assets including CSS, JavaScript, and images.
- `Models/`: Contains data models like `Event`, `AppState`, etc.
