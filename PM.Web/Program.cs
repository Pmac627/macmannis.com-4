using System.IO;
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

// https://blog.discountasp.net/3-ways-to-redirect-http-to-https-and-non-www-to-www-in-asp-net-core/
app.UseRewriter(new RewriteOptions()
    .AddRedirectToWww());

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
