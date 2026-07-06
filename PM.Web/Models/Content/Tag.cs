namespace PM.Web.Models.Content
{
    /// <summary>
    /// A technology/skill badge shown on a portfolio project's detail page. When <see cref="Color"/> is
    /// set, the view renders an inline background color; otherwise it falls back to the <c>badge-primary</c>
    /// CSS class.
    /// </summary>
    public record Tag(string Text, string Icon, string Color);
}
