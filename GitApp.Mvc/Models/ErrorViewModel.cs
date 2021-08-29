using System;

namespace GitApp.Mvc.Models
{
    /// <summary>
    /// Error View Model. In case of error this ViewModel is being used.
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
