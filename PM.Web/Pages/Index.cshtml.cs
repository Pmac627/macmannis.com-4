using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PM.Web.Models.Content;
using PM.Web.Services;

namespace PM.Web.Pages
{
    /// <summary>
    /// Page model for the home page. Renders every section of <see cref="SiteContent"/>.
    /// </summary>
    /// <seealso href="../../docs/flows/home-page-render.md">Home page render flow</seealso>
    public class IndexModel : PageModel
    {
        private readonly IContentService _contentService;

        /// <summary>
        /// Creates the page model.
        /// </summary>
        /// <param name="contentService">Source of the site's content.</param>
        public IndexModel(IContentService contentService)
        {
            ArgumentNullException.ThrowIfNull(contentService);

            _contentService = contentService;
        }

        /// <summary>
        /// The full site content graph, populated in <see cref="OnGet"/>.
        /// </summary>
        public SiteContent Site { get; private set; }

        /// <summary>
        /// Years of professional experience, computed as the current year minus <see cref="About.StartYear"/>.
        /// </summary>
        public int YearsExperience { get; private set; }

        /// <summary>
        /// Loads the site content and computes <see cref="YearsExperience"/> for this request.
        /// </summary>
        public void OnGet()
        {
            Site = _contentService.GetContent();
            YearsExperience = DateTime.Now.Year - Site.About.StartYear;
        }
    }
}
