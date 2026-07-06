using System;
using System.IO;

namespace PM.Web.Services
{
    /// <inheritdoc cref="IContentFileReader" />
    public sealed class ContentFileReader : IContentFileReader
    {
        private readonly string _path;

        /// <summary>
        /// Creates a reader bound to a specific file path.
        /// </summary>
        /// <param name="path">The absolute path to the content file, e.g. <c>content/site.json</c> under the app's content root.</param>
        public ContentFileReader(string path)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(path);

            _path = path;
        }

        /// <inheritdoc />
        /// <exception cref="FileNotFoundException">The file at the configured path does not exist.</exception>
        public string ReadAllText()
        {
            if (!File.Exists(_path))
            {
                throw new FileNotFoundException($"Content file not found at '{_path}'.", _path);
            }

            return File.ReadAllText(_path);
        }
    }
}
