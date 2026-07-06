# macmannis.com

Personal portfolio site for Pat MacMannis. Software Engineering. Done Right.

## Stack

- ASP.NET Core (.NET 10), C#, Razor Pages
- Content-driven: all page content is read from `PM.Web/content/site.json`
- Static front-end assets (CSS, JavaScript, images, self-hosted webfonts) served from `wwwroot`, no jQuery or third-party JS/CSS framework

See [docs/](docs/index.md) for architecture, request flows, and decision records.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code (optional)

## Run locally

```bash
dotnet run --project PM.Web
```

Browse to the address printed in the console (for example `https://localhost:61989`). In Visual Studio, open `MacMannisV4.sln` and press F5.

## Configuration

`PM.Web/appsettings.json` holds base, non-secret settings and is committed. Environment overrides in `appsettings.Development.json` and `appsettings.Production.json` are gitignored because they may contain secrets (SendGrid, reCAPTCHA). Create them locally as needed.

## Project structure

```
MacMannisV4.sln
PM.Web/
  Pages/            Razor Pages (Index, PortfolioDetails/{slug}, Privacy, Shared/_Layout, Shared/Error)
  Services/         IContentService / ContentService (loads and caches content/site.json)
  Models/           Content records (Models/Content/) and ErrorModel
  content/          site.json, the single source of truth for all page content
  wwwroot/          css, js, img, self-hosted webfonts
  appsettings.json  Base configuration
PM.Web.Tests/       xUnit + FakeItEasy tests for ContentService
docs/               Architecture, flows, and decision records (see docs/index.md)
```
