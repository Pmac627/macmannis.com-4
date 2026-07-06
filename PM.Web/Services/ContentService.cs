using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PM.Web.Models.Content;

namespace PM.Web.Services
{
    /// <inheritdoc cref="IContentService" />
    /// <remarks>
    /// Registered as a singleton (see <c>Program.cs</c>). Loads <c>content/site.json</c> lazily on first
    /// access via <see cref="IContentFileReader"/> and caches the result for the lifetime of the process;
    /// a missing file or malformed JSON fails fast by throwing out of <see cref="GetContent"/> instead of
    /// being swallowed.
    /// </remarks>
    /// <seealso href="../../docs/flows/content-loading.md">Content loading flow</seealso>
    public sealed class ContentService : IContentService
    {
        private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

        private readonly IContentFileReader _reader;
        private readonly ILogger<ContentService> _logger;
        private readonly Lazy<SiteContent> _content;

        /// <summary>
        /// Creates the service. Content is not read until the first call to <see cref="GetContent"/> or <see cref="GetProject"/>.
        /// </summary>
        /// <param name="reader">Abstraction over reading the content file, so tests can fake it without touching the filesystem.</param>
        /// <param name="logger">Logger used to record successful loads (Information) and load failures (Error).</param>
        public ContentService(IContentFileReader reader, ILogger<ContentService> logger)
        {
            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(logger);

            _reader = reader;
            _logger = logger;
            _content = new Lazy<SiteContent>(Load);
        }

        /// <inheritdoc />
        public SiteContent GetContent()
        {
            return _content.Value;
        }

        /// <inheritdoc />
        public PortfolioProject GetProject(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            foreach (var project in GetContent().Portfolio)
            {
                if (string.Equals(project.Slug, slug, StringComparison.OrdinalIgnoreCase))
                {
                    return project;
                }
            }

            return null;
        }

        private SiteContent Load()
        {
            try
            {
                var json = _reader.ReadAllText();
                var content = JsonSerializer.Deserialize<SiteContent>(json, SerializerOptions)
                    ?? throw new InvalidOperationException("Content file deserialized to null.");

                _logger.LogInformation("Loaded site content.");

                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load site content.");

                throw;
            }
        }
    }
}
