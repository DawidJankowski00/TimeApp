# TimeApp

Cross-platform app (.NET MAUI) for managing notes/tasks with deadlines and team sharing, with an ASP.NET Core REST API.
Early learning project (2023-2024).

## Features
- Register / log in (passwords hashed with Argon2)
- Create notes with start date, deadline and status
- Teams (added Jan 2024)

## Stack
.NET 7, .NET MAUI, MVVM (CommunityToolkit.Mvvm), ASP.NET Core Web API, Swagger, Azure App Service

## Run
1. Start the API: `cd TimeAppRestApi && dotnet run` (`users.json` and `group.json` are created on first use).
2. Set the API address in `TimeApp/Services/ApiConfig.cs`. The default points to the original Azure deployment, which may no longer be online.
3. Run `TimeApp` on Android or Windows from Visual Studio.

## What I would improve
Replace the JSON files with a database, add tests, add JWT auth.
