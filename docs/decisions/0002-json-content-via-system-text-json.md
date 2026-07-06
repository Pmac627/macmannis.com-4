---
title: "0002: JSON content via System.Text.Json"
type: explanation
status: current
last_reviewed: 2026-07-06
---

# 0002: JSON content via System.Text.Json

- Status: accepted
- Date: 2026-07-06

## Context

Before the redesign, all page content (hero text, about paragraphs, facts, skills, resume history, services, testimonials) was hardcoded inline in `Views/Home/Index.cshtml`, and the six portfolio projects were duplicated again as C# object literals in a `switch` inside `HomeController.PortfolioDetails`. Editing any piece of content required editing markup and/or C# and a full rebuild. `Newtonsoft.Json` was referenced in the `.csproj` but unused.

## Decision

We will store all structured site content in a single version-controlled file, `PM.Web/content/site.json`, deserialized with `System.Text.Json` (the framework-included serializer; `Newtonsoft.Json` was removed) into immutable C# records under `PM.Web/Models/Content/`. Long-form prose (project descriptions, the about section) is stored as string arrays (`Paragraphs`) rendered as separate `<p>` elements, replacing the old `@Html.Raw(...)` plus embedded `<br><br>` markers. Markdown is deliberately not used for this content, because a project record needs typed, relational fields (a slug, tagged icons with colors, a list of image objects) that Markdown cannot represent; Markdown is reserved for a possible future blog, where each post is genuinely prose-shaped.

## Consequences

Easier: adding, editing, or reordering a portfolio project, skill, testimonial, or resume entry is a JSON edit, not a markup/C# edit. The content model is testable in isolation (see `PM.Web.Tests/ContentServiceTests.cs`) without spinning up any web infrastructure. Net new NuGet dependencies for this: zero.

Harder: `site.json` is loaded and cached once per process (see the content-loading flow doc), so a content edit requires an app restart to take effect in a running environment — there is no hot-reload of content.

## Notes

None yet.
