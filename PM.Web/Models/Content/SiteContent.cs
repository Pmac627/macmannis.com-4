namespace PM.Web.Models.Content
{
    /// <summary>
    /// Root of the content graph deserialized from <c>content/site.json</c>. One instance is
    /// loaded and cached for the lifetime of the process by <see cref="PM.Web.Services.ContentService"/>.
    /// </summary>
    /// <seealso href="../../../docs/flows/content-loading.md">Content loading flow</seealso>
    public record SiteContent(
        Hero Hero,
        About About,
        Fact[] Facts,
        SkillItem[] Skills,
        ResumeSection Resume,
        Service[] Services,
        PortfolioProject[] Portfolio,
        Testimonial[] Testimonials);
}
