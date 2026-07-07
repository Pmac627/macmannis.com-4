namespace PM.Web.Models.Content
{
    /// <summary>
    /// One employer/role entry in <see cref="ResumeSection.Experience"/>. <see cref="Subtitle"/> is optional
    /// supporting text rendered below the organization and location, commonly used for acquisition context.
    /// </summary>
    /// <seealso href="../../../docs/flows/home-page-render.md">Home page render flow</seealso>
    public record ExperienceItem(
        string Title,
        string Period,
        string Org,
        string Location,
        string[] Bullets,
        string Subtitle = "");
}
