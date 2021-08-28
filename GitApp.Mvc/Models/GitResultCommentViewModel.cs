using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Models
{
    public class GitResultCommentViewModel
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public string FineComment { get; set; }
        public string SortedWords { get; set; }
    }
}
