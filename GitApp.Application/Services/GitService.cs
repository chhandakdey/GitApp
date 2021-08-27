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
        public Task<IEnumerable<Dictionary<string, string>>> GetCommitMessages()
        {
            throw new NotImplementedException();
        }
    }
}
