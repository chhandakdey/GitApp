using GitApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Git Repository Interface
    /// </summary>
    public interface IGitRepository
    {
        public Task<HttpResponseMessage> GetAllCommentsAsync(GitRequestDTO requestModel);
    }
}
