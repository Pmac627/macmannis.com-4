namespace PM.Web.Models.Content
{
    /// <summary>
    /// Speaking engagement content shown directly under the resume summary.
    /// </summary>
    /// <seealso href="../../../docs/flows/home-page-render.md">Home page render flow</seealso>
    public record SpeakingEngagements(string Description, string[] Bullets);
}
