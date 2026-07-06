---
title: Architecture
type: explanation
status: draft
last_reviewed: 2026-07-06
covers:
  - PM.Web/Program.cs
  - PM.Web/Pages/**
  - PM.Web/Services/**
  - PM.Web/Models/**
  - PM.Web/content/site.json
---

# Architecture

## C4 container view

```mermaid
C4Container
    title PM.Web container view
    Person(visitor, "Site visitor")
    System_Boundary(pmweb, "PM.Web") {
        Container(pages, "Razor Pages", "ASP.NET Core", "Index, PortfolioDetails/{slug}, Privacy, Shared/Error, Shared/_Layout")
        Container(contentsvc, "ContentService", "C# singleton", "Loads, caches, and queries site content")
        Container(staticassets, "wwwroot", "static files", "layout.css, fonts.css, macmannisv4.css, site.js, self-hosted webfonts, images")
        ContainerDb(json, "content/site.json", "JSON file", "Single source of truth for all page content")
    }
    System_Ext(cdn, "cdnjs", "Font Awesome CSS + webfont")

    Rel(visitor, pages, "HTTPS GET")
    Rel(pages, contentsvc, "GetContent() / GetProject(slug)")
    Rel(contentsvc, json, "Reads once, caches in memory")
    Rel(pages, staticassets, "Serves CSS/JS/images")
    Rel(visitor, cdn, "Browser loads Font Awesome directly")
```

## Content pipeline

Every page follows the same read path: an incoming request is routed to a Razor Page, the page model asks `IContentService` for data, and `ContentService` serves it from an in-memory cache that it built once from `content/site.json`.

```mermaid
flowchart LR
    A[HTTP request] --> B[Razor Page routing]
    B --> C[PageModel.OnGet]
    C --> D["IContentService.GetContent() / GetProject(slug)"]
    D --> E{"Already loaded?"}
    E -- no --> F["ContentFileReader.ReadAllText()\ncontent/site.json"]
    F --> G["JsonSerializer.Deserialize&lt;SiteContent&gt;"]
    G --> H["Lazy&lt;SiteContent&gt; cache"]
    E -- yes --> H
    H --> D
    D --> I[Razor view renders SiteContent]
    I --> J[HTML response]
```

## Key components

- **Hosting** ([Program.cs](../PM.Web/Program.cs)): minimal-hosting `WebApplication`. Registers `AddRazorPages()`, a singleton `IContentFileReader` pointed at `content/site.json` under the app's content root, and a singleton `IContentService`. Pipeline: dev-exception-page or `UseExceptionHandler("/Error")` + HSTS, a rewrite rule that redirects to the `www` host, static files, routing, authorization, then `MapRazorPages()`.
- **Content model** ([Models/Content/](../PM.Web/Models/Content/)): immutable C# records (`SiteContent`, `Hero`, `About`, `Fact`, `SkillItem`, `ResumeSection`, `ExperienceItem`, `Service`, `PortfolioProject`, `Testimonial`, `MediaImage`, `Tag`) that mirror the shape of `content/site.json` field-for-field, deserialized with `System.Text.Json`'s web defaults (camelCase).
- **`ContentService`** ([Services/ContentService.cs](../PM.Web/Services/ContentService.cs)): the only place that reads `site.json`. Loads and deserializes lazily on first access (`Lazy<SiteContent>`), so the file is read at most once per process lifetime (the service is registered singleton). Fails fast: a missing file or malformed JSON throws out of `Load()` instead of being swallowed, and is logged at Error; a successful load logs Information once. `GetProject(slug)` does a case-insensitive linear scan over `Portfolio` and returns `null` for no match, letting the caller decide what "not found" means.
- **Pages** ([Pages/](../PM.Web/Pages/)): `Index` renders every section of `SiteContent` via `foreach` loops (no per-item hardcoding); `PortfolioDetails` routes on `{slug}` and redirects to `Index` when `GetProject` returns null; `Privacy` is static; `Shared/_Layout` injects `IContentService` directly (via `@inject`) to render the hero name/roles and footer, since those appear outside `Index`'s own body; `Shared/Error` is mapped to the fixed route `/Error` to match `UseExceptionHandler("/Error")`.
- **Front end** ([wwwroot/](../PM.Web/wwwroot/)): no jQuery or third-party JS/CSS framework. `site.js` is a single vanilla-JS file covering preloader, typewriter, smooth scroll/scrollspy, mobile nav, back-to-top, `IntersectionObserver`-driven counters/skill-bars/scroll-reveal, a portfolio category filter, a `<dialog>`-based lightbox, and a small custom carousel. `layout.css` supplies the Bootstrap reboot, responsive container, grid, and utility rules the pages use; `fonts.css` declares `@font-face` rules for the self-hosted Open Sans/Raleway/Poppins woff2 files in `wwwroot/fonts/`; `macmannisv4.css` is the original template's component styling, carried forward. Font Awesome is loaded from the cdnjs CDN rather than self-hosted or from FontAwesome's account-linked kit script; see [decisions/0004-fontawesome-via-cdn.md](decisions/0004-fontawesome-via-cdn.md).

## Testing

`PM.Web.Tests` (xUnit + FakeItEasy) covers `ContentService` end to end against a fake `IContentFileReader`: valid-JSON mapping, missing-file and malformed-JSON fail-fast behavior, single-read caching across repeated `GetContent()` calls, and case-insensitive slug lookup including the not-found path.
