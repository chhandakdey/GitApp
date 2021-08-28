using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.DTOs
{
    public class GitRequestDTO
    {
        public string Username { get; set; }       
        public string RepoUrl { get; set; }       
        public string AccessToken { get; set; }
    }
}
