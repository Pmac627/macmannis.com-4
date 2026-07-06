namespace PM.Web.Models
{
    /// <summary>
    /// Data shown on the error page: the current request's trace identifier, if any.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// The request/trace identifier to display, or empty if none is available.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Whether <see cref="RequestId"/> has a value worth displaying.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
