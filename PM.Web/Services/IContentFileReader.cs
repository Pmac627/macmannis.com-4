namespace PM.Web.Services
{
    /// <summary>
    /// Abstraction over reading the raw content file, so <see cref="ContentService"/> can be tested
    /// without a real filesystem.
    /// </summary>
    /// <seealso href="../../docs/flows/content-loading.md">Content loading flow</seealso>
    public interface IContentFileReader
    {
        /// <summary>
        /// Reads the full text of the content file.
        /// </summary>
        /// <returns>The file's raw text content.</returns>
        string ReadAllText();
    }
}
