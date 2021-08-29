using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Models
{
    /// <summary>
    /// Result View Model
    /// </summary>
    public class GitResultCommentViewModel
    {
        [DisplayName("Id")]
        public long Id { get; set; }
        [DisplayName("Comment")]
        public string Comment { get; set; }
        [DisplayName("Sorted Words")]
        public string SortedWords { get; set; }
    }
}
