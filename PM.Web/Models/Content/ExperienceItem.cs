namespace PM.Web.Models.Content
{
    /// <summary>
    /// One employer/role entry in <see cref="ResumeSection.Experience"/>.
    /// </summary>
    public record ExperienceItem(
        string Title,
        string Period,
        string Org,
        string Location,
        string[] Bullets);
}
