# CineVerse

A film & TV search engine built with **Blazor Server** and **ASP.NET Core Web API**.  
The project has a dual purpose: deliver a fully filterable catalogue (genre, rating, provider, certification, region, …) and serve as a hands-on playground for applying Clean Code, modular architecture, and modern C# practices.

---

## Tech stack

| Layer         | Technologies                                            |
| ------------- | ------------------------------------------------------- |
| Front-end     | Blazor Server, vanilla CSS, RestSharp                   |
| Back-end      | ASP.NET Core Web API, Entity Framework Core, SQL Server |
| Integrations  | TMDB API (film / TV metadata)                           |
| Shared model  | **CineVerse.shared** project (DTOs, value objects, helpers) |

---

## Architecture at a glance

- **Three-project solution**  
  `CineVerse.client` (UI) · `CineVerse.api` (API) · `CineVerse.shared` (shared models)

- **Razor component separation**  
  `*.razor` (markup) · `*.razor.cs` (logic) · `*.razor.css` (style)

- **Feature folders** keep related pages, components, services, and view-models together.

- **Zero external UI libraries**: full DOM control; styling handled with internal utility classes and CSS variables.

---

## Running locally

### Prerequisites
- .NET 8 SDK  
- SQL Server (or Express) running  
- TMDB API key (`TMDB_API_KEY`)

### Quick start

```bash
git clone https://github.com/C-Gian/CineVerse.git
cd CineVerse
git checkout develop    # main working branch

# API: create your local settings
cp CineVerse.api/appsettings.Development.json.example CineVerse.api/appsettings.Development.json
# edit connection string + TMDB_API_KEY

dotnet build                    # compile everything
dotnet ef database update --project CineVerse.api   # apply migrations

# run (two terminals)
dotnet run --project CineVerse.api
dotnet run --project CineVerse.client
