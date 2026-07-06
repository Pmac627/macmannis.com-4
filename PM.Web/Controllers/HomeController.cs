using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PM.Web.ViewModels;
using System;
using System.Diagnostics;

namespace PM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public string CaptchaClientKey { get; set; }

        public HomeController(IConfiguration config, ILogger<HomeController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("/")]
        [HttpGet("/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Home/PortfolioDetails/{projectName}")]
        public IActionResult PortfolioDetails(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                return RedirectToAction("Index");
            }

            return (projectName.ToUpperInvariant()) switch
            {
                "2TO1" => View(new ProductDetailsVM
                {
                    Client = "Metragenix",
                    Category = "Web Development",
                    Date = "December 2012",
                    Description = @"Metragenix 2-to-1 Protein Bars (MG) offers a line of high-quality protein bars that are specially formulated to have a solid ration of protein to net carbs. I was an employee of IMS during this project.<br><br>
MG provided the design via PSD and basic descriptions of how the UI should function. I provided a clear explanation of the process and how long each step would take to complete. The development time was approximately 120 hours which included coding, QA, UAT and deployment.<br><br>
2to1proteinbars.com is a simple standards-compliant website created with HTML5, CSS3, JavaScript, jQuery and Nivo Slider (a jQuery Plugin). The back end is powered by PHP 5. The site is also mobile-friendly and the products are dynamically displayed without a database.",
                    Title = "Metragenix 2-to-1 Protein Bars",
                    Uri = null,
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/2-to-1-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("PHP", "fab fa-php", "#355e3b"),
                        new TagVM("HTML5", "fab fa-html5", "#355e3b"),
                        new TagVM("CSS", "fab fa-css3-alt", "#355e3b"),
                        new TagVM("JS", "fab fa-js-square", "#355e3b"),
                        new TagVM("JQuery", "fab fa-js-square", "#355e3b")
                    ]
                }),
                "TP" => View(new ProductDetailsVM
                {
                    Client = "TheraPartners, LLC",
                    Category = "Web Development",
                    Date = "March 2017",
                    Description = "I created a portfolio website to showcase the unique TheraPartners service for children. The site was created with ASP.NET MVC 5, MSSQL and Bootstrap 3. The site features a list of their services as well as a contact form.",
                    Title = "TheraPartners",
                    Uri = null,
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/therapartners-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("C#.NET", "fab fa-microsoft", "#355e3b"),
                        new TagVM("SQL", "fas fa-database", "#355e3b"),
                        new TagVM("MVC5", "fab fa-microsoft", "#355e3b"),
                        new TagVM("Bootstrap", "fab fa-bootstrap", "#355e3b"),
                        new TagVM("CSS", "fab fa-css3-alt", "#355e3b"),
                        new TagVM("JS", "fab fa-js-square", "#355e3b"),
                        new TagVM("HTML5", "fab fa-html5", "#355e3b"),
                        new TagVM("JQuery", "fab fa-js-square", "#355e3b")
                    ]
                }),
                "SS" => View(new ProductDetailsVM
                {
                    Client = "Sourcing Services, LLC",
                    Category = "Web Development",
                    Date = "December 2012",
                    Description = "",
                    Title = "Sourcing Services",
                    Uri = null,
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/sourcing-services-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("PHP", "fab fa-php", "#355e3b"),
                        new TagVM("HTML5", "fab fa-html5", "#355e3b"),
                        new TagVM("CSS", "fab fa-css3-alt", "#355e3b"),
                        new TagVM("JS", "fab fa-js-square", "#355e3b"),
                        new TagVM("JQuery", "fab fa-js-square", "#355e3b")
                    ]
                }),
                "CSEM" => View(new ProductDetailsVM
                {
                    Client = "Center for Safety &amp; Environmental Management",
                    Category = "Application Upgrade",
                    Date = "July 2014",
                    Description = "",
                    Title = "Center for Safety &amp; Environmental Management",
                    Uri = null,
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/csem-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("PHP", "fab fa-php", "#355e3b"),
                        new TagVM("HTML5", "fab fa-html5", "#355e3b"),
                        new TagVM("CSS", "fab fa-css3-alt", "#355e3b"),
                        new TagVM("JS", "fab fa-js-square", "#355e3b"),
                        new TagVM("JQuery", "fab fa-js-square", "#355e3b")
                    ]
                }),
                "OMH" => View(new ProductDetailsVM
                {
                    Client = "Sisters of Mary Immaculate",
                    Category = "Web Development",
                    Date = "August 2009",
                    Description = @"Our Mission House Publishing (OMH) is the entity that the Sisters of Mary Immaculate (SMI) offer their line of Catholic children's literature and products.<br><br>
I met with the organization representatives that outlined what they were looking for. I provided a few mock ups of potential designs that included the white and light blue design they chose, which is based off of the habit that they typically wear. I also provided a clear explanation of the process and how long each step would take to complete. The development time was approximately 80 hours which included coding, QA, UAT and deployment.<br><br>
omhsmi.org is a simple standards-compliant website created with HTML5, CSS3 and Bootstrap 3. The back end is powered by ASP.NET MVC 5 and MSSQL. The site is mobile-friendly and utilizes metadata (http://schema.org). The site has seen two major updates since it was originally created in 2009 with PHP 5, HTML5, CSS3, JavaScript and MySQL. In 2013, the vanilla CSS and JavaScript was replaced with Bootstrap 3. In 2017, the back end was rebuilt using ASP.NET MVC 5 and MSSQL.",
                    Title = "Our Mission House Publishing",
                    Uri = new Uri("https://www.omhsmi.org/"),
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/omhsmi-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("C#.NET", "fab fa-microsoft", "#355e3b"),
                        new TagVM("SQL", "fas fa-database", "#355e3b"),
                        new TagVM("MVC5", "fab fa-microsoft", "#355e3b"),
                        new TagVM("Bootstrap", "fab fa-bootstrap", "#355e3b"),
                        new TagVM("CSS", "fab fa-css3-alt", "#355e3b"),
                        new TagVM("JS", "fab fa-js-square", "#355e3b"),
                        new TagVM("HTML5", "fab fa-html5", "#355e3b")
                    ]
                }),
                "IS" => View(new ProductDetailsVM
                {
                    Client = "Industrial Scientific Corporation",
                    Category = "Database Development",
                    Date = "May 2015",
                    Description = "",
                    Title = "Industrial Scientific",
                    Uri = null,
                    Images =
                    [
                        new ImageVM {
                            Title = "",
                            AltText = "",
                            Uri = new Uri("/img/portfolio/isc-preview.png", UriKind.Relative)
                        }
                    ],
                    Tags =
                    [
                        new TagVM("VBA", "fab fa-microsoft", "#355e3b"),
                        new TagVM("SQL", "fas fa-database", "#355e3b"),
                    ]
                }),
                _ => RedirectToAction("Index"),
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}