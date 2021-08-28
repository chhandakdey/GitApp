using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.DTOs
{
    public class GitResultCommentDTO
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public string SortedWords { get; set; }
    }
}
