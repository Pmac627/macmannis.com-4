using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PM.Web.Models.Content;
using PM.Web.Services;

namespace PM.Web.Pages
{
    /// <summary>
    /// Page model for a single portfolio project's detail page, routed at <c>/PortfolioDetails/{slug}</c>.
    /// </summary>
    /// <seealso href="../../docs/flows/portfolio-details.md">Portfolio details flow</seealso>
    public class PortfolioDetailsModel : PageModel
    {
        private readonly IContentService _contentService;

        /// <summary>
        /// Creates the page model.
        /// </summary>
        /// <param name="contentService">Source of the site's content.</param>
        public PortfolioDetailsModel(IContentService contentService)
        {
            ArgumentNullException.ThrowIfNull(contentService);

            _contentService = contentService;
        }

        /// <summary>
        /// The project being displayed, populated in <see cref="OnGet"/> when the slug resolves.
        /// </summary>
        public PortfolioProject Project { get; private set; }

        /// <summary>
        /// Resolves <paramref name="slug"/> to a project and renders the page, or redirects to
        /// <c>Index</c> when the slug does not match any project.
        /// </summary>
        /// <param name="slug">The project slug from the route.</param>
        public IActionResult OnGet(string slug)
        {
            var project = _contentService.GetProject(slug);

            if (project is null)
            {
                return RedirectToPage("Index");
            }

            Project = project;

            return Page();
        }
    }
}
