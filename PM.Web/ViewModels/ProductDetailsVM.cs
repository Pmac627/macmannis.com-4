using System;

namespace PM.Web.ViewModels
{
    public class ProductDetailsVM
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Client { get; set; }
        public string Date { get; set; }
        public Uri Uri { get; set; }
        public string Description { get; set; }
        public ImageVM[] Images { get; set; }
        public TagVM[] Tags { get; set; }
    }
}