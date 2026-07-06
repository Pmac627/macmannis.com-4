using PM.Web.Models.Content;

namespace PM.Web.Services
{
    /// <summary>
    /// Loads and serves the site's content, sourced from <c>content/site.json</c>.
    /// </summary>
    /// <seealso href="../../docs/flows/content-loading.md">Content loading flow</seealso>
    public interface IContentService
    {
        /// <summary>
        /// Returns the full site content graph, loading and caching it on first call.
        /// </summary>
        SiteContent GetContent();

        /// <summary>
        /// Looks up a portfolio project by its slug, case-insensitively.
        /// </summary>
        /// <param name="slug">The project slug, e.g. <c>"tp"</c>.</param>
        /// <returns>The matching <see cref="PortfolioProject"/>, or <see langword="null"/> if <paramref name="slug"/> is null/whitespace or no project matches.</returns>
        PortfolioProject GetProject(string slug);
    }
}
