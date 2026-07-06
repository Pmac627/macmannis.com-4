namespace PM.Web.Models.Content
{
    /// <summary>
    /// An image reference with alt text and title, used for portfolio thumbnails and detail-page galleries.
    /// </summary>
    public record MediaImage(string Uri, string Alt, string Title);
}
