---
title: "0004: Font Awesome via CDN, not self-hosted"
type: explanation
status: current
last_reviewed: 2026-07-06
---

# 0004: Font Awesome via CDN, not self-hosted

- Status: accepted
- Date: 2026-07-06

## Context

`_Layout.cshtml` and `PortfolioDetails.cshtml` loaded Font Awesome from `kit.fontawesome.com/6a66ab6d65.js`, an account-linked kit script tied to a specific FontAwesome.com account. The redesign's stated goal was to remove that account coupling. A self-hosted subset of the Font Awesome Free webfont (covering only the ~33 distinct icon classes actually used across the site) was built and verified working during the redesign, using the two required glyph files (`fa-solid-900.woff2`, `fa-brands-400.woff2`, ~274 KB combined for the full un-subsetted files, since true glyph subsetting would have needed a Python `fonttools` toolchain not otherwise used in this repo).

## Decision

We will load Font Awesome from a generic, non-account-linked CDN (cdnjs: `cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css`, pinned with a Subresource Integrity hash) rather than self-hosting the webfont files. This was an explicit judgment call during the redesign session: CDNs are an acceptable dependency for shared, widely-cached libraries, and full self-hosting is not required in every case — the actual problem the plan called out was the account coupling of the `kit.fontawesome.com` script, not the use of a CDN per se.

## Consequences

Easier: no font files to vendor or update in this repo; cdnjs is a widely-used, publicly cacheable host with no account tie-in, so the specific problem (account coupling) is solved. The SRI hash pins the exact file content, so a compromised or altered CDN response would fail the browser's integrity check rather than load silently.

Harder: the site still makes one external network call for icons (Font Awesome's CSS references its own webfont on the same CDN), so it is not a zero-external-dependency page; a cdnjs outage would leave the page without icons (degrades gracefully — the rest of the page still renders — but icons would be missing). This differs from Google Fonts, which was fully self-hosted in the same pass (see [0003](0003-vanilla-front-end-no-jquery.md)); the two were not treated identically because Font Awesome's full webfont is materially larger to vendor without a subsetting tool, while the three Google Font families needed were a small, fixed set of weights already isolated by the existing `fonts.googleapis.com` URL.

## Notes

None yet.
