namespace PM.Web.Models.Content
{
    /// <summary>
    /// The hero banner's name and rotating role/tagline text, rendered by <c>Pages/Shared/_Layout.cshtml</c>.
    /// </summary>
    public record Hero(string Name, string[] Roles);
}
