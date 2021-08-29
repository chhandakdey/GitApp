using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.DTOs
{
    /// <summary>
    /// GIT Comment DTO
    /// </summary>
    public class GitCommentDTO
    {
        public long Id { get; set; }
        public string Message { get; set; }
    }
}
