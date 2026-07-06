namespace PM.Web.Models.Content
{
    /// <summary>
    /// A key-technology entry in the Skills section. <see cref="StartYear"/> is subtracted from the
    /// current year to compute the "years" figure shown next to each skill.
    /// </summary>
    public record SkillItem(string Name, string Icon, int StartYear);
}
