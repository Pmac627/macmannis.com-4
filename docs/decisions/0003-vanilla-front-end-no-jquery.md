---
title: "0003: Vanilla front end, no jQuery"
type: explanation
status: current
last_reviewed: 2026-07-06
---

# 0003: Vanilla front end, no jQuery

- Status: accepted
- Date: 2026-07-06

## Context

`wwwroot/lib` held jQuery plus eleven plugins (Bootstrap, AOS, Waypoints, CountUp, Isotope, Venobox, Owl Carousel, Typed.js, jQuery Easing, jQuery Validation + unobtrusive, Boxicons) totaling roughly 6.7 MB, for a one-page site with no form to validate (jQuery Validation and Boxicons were dead weight — Boxicons wasn't even referenced anywhere).

## Decision

We will remove `wwwroot/lib` entirely and reimplement every behavior it provided in a single vanilla-JS file, `wwwroot/js/site.js`: preloader fade-out, a hand-rolled typewriter for the hero tagline, smooth scroll and scrollspy for the nav menu, a mobile-nav toggle, a back-to-top button, `IntersectionObserver`-driven fact counters and skill progress bars, `IntersectionObserver`-driven scroll-reveal (replacing AOS), a `data-filter`-based portfolio category filter (replacing Isotope), a native `<dialog>`-based lightbox (replacing Venobox), and a small custom carousel used for both the testimonials section and the portfolio-details image gallery (replacing Owl Carousel). The Bootstrap reboot, grid, and utility behavior actually used by the markup were reimplemented as a small hand-written `wwwroot/css/layout.css`, rather than kept as a dependency.

## Consequences

Easier: the page ships with one first-party JS file and no third-party runtime JS dependency; there is far less to update or audit for vulnerabilities, and the behavior is easy to read end to end in one file (`site.js`) instead of across a dozen plugin APIs.

Harder: any new interactive behavior now has to be hand-written instead of reached for from an existing plugin; `layout.css` only covers the reboot, grid, and utility rules the current pages actually use; it is not a general-purpose Bootstrap replacement, so adding a page that needs a rule not already in `layout.css` requires extending it.

## Notes

Font Awesome (icon font) and Google Fonts (webfonts) were also removed from `_Layout.cshtml`'s external script/link tags as part of the same front-end cleanup, but via different mechanisms — see [0004](0004-fontawesome-via-cdn.md) for Font Awesome specifically. Google Fonts was fully self-hosted (`wwwroot/fonts/*.woff2` + `wwwroot/css/fonts.css`), not kept on a CDN.
