namespace PM.Web.Models.Content
{
    /// <summary>
    /// A single portfolio project, shown as a card on the home page and in full on
    /// <c>Pages/PortfolioDetails.cshtml</c>. <see cref="Slug"/> is matched case-insensitively by
    /// <see cref="PM.Web.Services.IContentService.GetProject"/>.
    /// </summary>
    /// <seealso href="../../../docs/flows/portfolio-details.md">Portfolio details flow</seealso>
    public record PortfolioProject(
        string Slug,
        string Title,
        string Category,
        string Client,
        string Date,
        string ProjectUri,
        string[] Paragraphs,
        MediaImage Thumbnail,
        MediaImage[] Images,
        Tag[] Tags);
}
