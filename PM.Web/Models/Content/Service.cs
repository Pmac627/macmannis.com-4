namespace PM.Web.Models.Content
{
    /// <summary>
    /// One offering in the Services section. <see cref="ColorClass"/> selects both a CSS color variant
    /// and the matching decorative SVG icon background looked up in <c>Pages/Index.cshtml</c>.
    /// </summary>
    public record Service(string Icon, string ColorClass, string Title, string Description);
}
