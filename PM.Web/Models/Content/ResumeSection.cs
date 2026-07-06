namespace PM.Web.Models.Content
{
    /// <summary>
    /// The Resume section: a short summary, speaking engagements, education details, certifications,
    /// and the professional experience history.
    /// </summary>
    /// <seealso href="../../../docs/flows/home-page-render.md">Home page render flow</seealso>
    public record ResumeSection(
        string Summary,
        SpeakingEngagements SpeakingEngagements,
        EducationItem Education,
        string[] Certifications,
        ExperienceItem[] Experience);
}
