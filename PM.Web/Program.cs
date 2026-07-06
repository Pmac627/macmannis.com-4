using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PM.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var contentPath = Path.Combine(builder.Environment.ContentRootPath, "content", "site.json");
builder.Services.AddSingleton<IContentFileReader>(new ContentFileReader(contentPath));
builder.Services.AddSingleton<IContentService, ContentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var cspNonce = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    context.Items["CspNonce"] = cspNonce;

    context.Response.Headers.XContentTypeOptions = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers.XFrameOptions = "DENY";
    context.Response.Headers.ContentSecurityPolicy = BuildContentSecurityPolicy(cspNonce);

    await next().ConfigureAwait(false);
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// https://blog.discountasp.net/3-ways-to-redirect-http-to-https-and-non-www-to-www-in-asp-net-core/
app.UseRewriter(new RewriteOptions()
    .AddRedirectToWww());

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

static string BuildContentSecurityPolicy(string nonce)
{
    return string.Join(
        "; ",
        "default-src 'self'",
        "base-uri 'self'",
        "object-src 'none'",
        "frame-ancestors 'none'",
        "form-action 'self'",
        "img-src 'self' data: https://*.gosquared.com",
        "style-src 'self' 'unsafe-inline' https://cdnjs.cloudflare.com",
        "font-src 'self' https://cdnjs.cloudflare.com",
        $"script-src 'self' 'nonce-{nonce}' https://d1l6p2sc9645hc.cloudfront.net",
        "connect-src 'self' https://*.gosquared.com wss://*.gosquared.com");
}
