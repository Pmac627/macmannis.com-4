namespace PM.Web.Models.Content
{
    /// <summary>
    /// The About section: a title, prose paragraphs, a list of certifications, and the year professional
    /// experience is counted from. <see cref="StartYear"/> feeds the "N years of experience" figure computed
    /// at request time by <c>IndexModel.OnGet()</c>.
    /// </summary>
    public record About(string Title, string[] Paragraphs, string[] Certifications, int StartYear);
}
