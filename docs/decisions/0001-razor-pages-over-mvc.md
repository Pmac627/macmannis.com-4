---
title: "0001: Razor Pages over MVC"
type: explanation
status: current
last_reviewed: 2026-07-06
---

# 0001: Razor Pages over MVC

- Status: accepted
- Date: 2026-07-06

## Context

The app was an MVC app (`HomeController`, `Views/Home/*.cshtml`) with a single controller holding three actions (`Index`, `PortfolioDetails`, `Privacy`) and no other MVC-specific need (no API controllers, no complex routing). The redesign's stated goals were to move content out of markup/code and into JSON, and to leave room for two features not built yet: a file-based blog and a server-backed contact form.

## Decision

We will use ASP.NET Core Razor Pages instead of MVC. Each user-facing page (`Index`, `PortfolioDetails`, `Privacy`) becomes a page + page-model pair under `Pages/`, registered via `AddRazorPages()` / `MapRazorPages()` in `Program.cs`. `Controllers/`, `Views/`, and `ViewModels/` were deleted.

## Consequences

Easier: the 1:1 page-to-route mapping fits this app's shape (a handful of independent pages, not a resource-oriented API), and Razor Pages' `PageModel` per page keeps each page's logic co-located with its markup instead of centralized in one controller. A future blog can add pages under `Pages/Blog/` without touching existing routes; a future contact form is a natural additional page + `OnPost` handler.

Harder: the portfolio-details route changed from `/Home/PortfolioDetails/{slug}` (MVC controller/action routing) to `/PortfolioDetails/{slug}` (Razor Pages' `@page "{slug}"` route on `Pages/PortfolioDetails.cshtml`). No redirect was added from the old path — see the portfolio-details flow doc for that call.

## Notes

None yet.
