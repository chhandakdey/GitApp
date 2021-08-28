using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Services
{
    public class GitService : IGitService
    {
        public IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> Dao { get; private set; }

        public GitService(IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> dao)
        {
            Dao = dao;
        }
        public async Task<IEnumerable<GitCommentDTO>> GetCommitMessagesAsync(GitRequestDTO model)
        {
            return await Dao.GetAllAsync(model);
        }
    }
}
