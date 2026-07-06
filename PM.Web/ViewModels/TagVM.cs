namespace PM.Web.ViewModels
{
    public class TagVM
    {
        public TagVM(string text, string icon, string color)
        {
            Text = text;
            Icon = icon;
            Color = color;
        }

        public string Text { get; init; }
        public string Icon { get; init; }
        public string Color { get; init; }
    }
}