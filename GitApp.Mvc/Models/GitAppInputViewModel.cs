using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Models
{
    public class GitAppInputViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RepoUrl { get; set; }
        [Required]
        public string AccessToken { get; set; }

    }
}
