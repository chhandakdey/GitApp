using GitApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Interfaces
{
    public interface IGitService
    {
        public Task<IEnumerable<GitCommentDTO>> GetCommitMessagesAsync(GitRequestDTO requestDTO);
    }
}
