namespace PM.Web.Models.Content
{
    /// <summary>
    /// One "facts" count-up box (e.g. "50+ Unique Clients"). Does not include the "years of experience"
    /// fact, which is computed from <see cref="About.StartYear"/> rather than stored statically.
    /// </summary>
    public record Fact(string Icon, int Value, string Label, int DurationSeconds);
}
