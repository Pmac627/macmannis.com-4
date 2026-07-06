namespace PM.Web.Models.Content
{
    /// <summary>
    /// Education entry shown in the resume section, rendered similarly to professional experience items.
    /// </summary>
    /// <seealso href="../../../docs/flows/home-page-render.md">Home page render flow</seealso>
    public record EducationItem(
        string Major,
        string MinorOrCertificate,
        string Period,
        string Location);
}
