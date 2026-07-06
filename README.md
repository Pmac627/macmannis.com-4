# macmannis.com

Personal portfolio site for Pat MacMannis. Software Engineering. Done Right.

## Stack

- ASP.NET Core (.NET 10), C#
- Server-rendered Razor views
- Static front-end assets (CSS, JavaScript, images) served from `wwwroot`

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
  Controllers/      Request handling
  Views/            Razor views
  ViewModels/       View models
  wwwroot/          css, js, img, and third-party libraries
  appsettings.json  Base configuration
```
