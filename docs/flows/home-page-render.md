---
title: Home page render
type: explanation
status: draft
last_reviewed: 2026-07-06
covers:
  - PM.Web/Pages/Index.cshtml
  - PM.Web/Pages/Index.cshtml.cs
  - PM.Web/Pages/Shared/_Layout.cshtml
---

# Home page render

## Purpose

Renders the single-page portfolio (hero, about, facts, skills, resume, portfolio grid, services, testimonials) entirely from `SiteContent`, with no hardcoded per-item markup. This is the flow that replaced the old MVC view's inline HTML plus the controller's 160-line project switch.

## Entry points

`GET /` and `GET /Index`, routed by Razor Pages convention to `Pages/Index.cshtml` / `IndexModel.OnGet()` ([Index.cshtml.cs](../../PM.Web/Pages/Index.cshtml.cs)).

## Sequence

```mermaid
sequenceDiagram
    actor Visitor
    participant Layout as _Layout.cshtml
    participant Page as IndexModel
    participant Svc as IContentService
    Visitor->>Layout: GET /
    Layout->>Svc: GetContent() (for hero name/roles, footer)
    Layout->>Page: RenderBody()
    Page->>Svc: GetContent()
    Svc-->>Page: SiteContent
    Page-->>Layout: Site, YearsExperience
    Layout-->>Visitor: full HTML page
```

## Key behavior

- `IndexModel.OnGet()` ([Index.cshtml.cs:40-44](../../PM.Web/Pages/Index.cshtml.cs)) fetches `SiteContent` once via `_contentService.GetContent()` and computes `YearsExperience` as `DateTime.Now.Year - Site.About.StartYear` — the "N years of professional experience" figure is derived at request time, not stored as a static number in `site.json`.
- `Index.cshtml` loops over every collection in `SiteContent` (`Facts`, `Skills`, `Resume.Experience`, `Portfolio`, `Services`, `Testimonials`) with `foreach`, so adding, removing, or reordering an item in `content/site.json` changes the rendered page with no code change.
- The resume summary supports the `{years}` token; `Index.cshtml` replaces that token with `YearsExperience`, then renders `Resume.SpeakingEngagements.Description` and its `Bullets` directly under the summary block.
- Resume education is structured as `major`, `minorOrCertificate`, `period`, and `location`; the first two render as separate `h4` lines, followed by the period and location in the same visual pattern as experience entries.
- Resume timeline spacing is intentional: summary and education use the base `.resume-item` without bottom padding, while professional experience entries add `.resume-item-experience` to keep the spacing between repeated roles.
- Fact counter spans emit each fact's `durationSeconds` value as `data-duration-ms`; `wwwroot/js/site.js` uses that value to control each count-up animation duration.
- The hero background uses `#hero:before` as a translucent overlay; `#hero .container` is positioned above that overlay so the hero name, typed text, and social links retain the intended contrast.
- Per-skill years (`skillYears`) are computed in the view as `currentYear - skill.StartYear`, floored at zero for future-dated content, so each key technology shows years since that technology's own start year.
- `layout.css` carries the Bootstrap reboot rules that affect page rhythm, including heading, paragraph, and list margins; this keeps section spacing aligned with the pre-redesign Bootstrap-rendered site.
- The Services section's decorative SVG icon backgrounds are not content data; they are looked up in the view by `service.ColorClass` via a local `@functions` helper (`ServiceIconBlob`), because they are presentational, not informational.
- `_Layout.cshtml` injects `IContentService` directly (`@inject`) to read `Hero` for the hero section and footer, since both live outside `Index`'s own `@RenderBody()` content and are shared across every page that uses this layout.
- Portfolio cards use `data-filter` values (`filter-web`, `filter-app`, `filter-db`) derived from `project.Category` in the view; the client-side filter in `site.js` reads these attributes (see [content-loading.md](content-loading.md) for how the underlying data arrives, and `wwwroot/js/site.js` for the filter implementation).

## Decisions

- [0001: Razor Pages over MVC](../decisions/0001-razor-pages-over-mvc.md)
- [0002: JSON content via System.Text.Json](../decisions/0002-json-content-via-system-text-json.md)
- [0003: Vanilla front end, no jQuery](../decisions/0003-vanilla-front-end-no-jquery.md)

## Source references

`PM.Web/Pages/Index.cshtml`, `PM.Web/Pages/Index.cshtml.cs`, `PM.Web/Pages/Shared/_Layout.cshtml`, `PM.Web/Models/Content/SiteContent.cs` and its nested records.

## Failure modes and edge cases

If `content/site.json` is missing or malformed, `ContentService.GetContent()` throws (see [content-loading.md](content-loading.md)); there is no fallback content, so the page will fail rather than render a partial/empty page. The `_gs` GoSquared analytics snippet and Font Awesome CDN `<link>` in `_Layout.cshtml` are the only network calls outside the app itself; there is no error handling around either failing to load (the page still renders, just without icons/analytics).
