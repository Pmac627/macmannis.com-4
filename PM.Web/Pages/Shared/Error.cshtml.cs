using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PM.Web.Models;

namespace PM.Web.Pages.Shared
{
    /// <summary>
    /// Page model for the error page mapped at <c>/Error</c>, used by <c>UseExceptionHandler("/Error")</c>
    /// in <c>Program.cs</c>. Disables response caching so a stale error page is never served.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorPageModel : PageModel
    {
        /// <summary>
        /// The error details for this request, populated in <see cref="OnGet"/>.
        /// </summary>
        public ErrorModel Error { get; private set; }

        /// <summary>
        /// Captures the current request/trace identifier to show on the error page.
        /// </summary>
        public void OnGet()
        {
            Error = new ErrorModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
        }
    }
}
