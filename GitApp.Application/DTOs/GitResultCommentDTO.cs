using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.DTOs
{
    /// <summary>
    /// GIT Result Comment DTO
    /// </summary>
    public class GitResultCommentDTO
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public string SortedWords { get; set; }
    }
}
