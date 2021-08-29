﻿using GitApp.Application.DTOs;
using GitApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Interfaces
{
    /// <summary>
    /// GIT Service Interface
    /// </summary>
    public interface IGitService
    {
        public Task<IEnumerable<GitResultCommentDTO>> GetCommitMessagesAsync(GitRequestDTO requestDTO);
    }
}
