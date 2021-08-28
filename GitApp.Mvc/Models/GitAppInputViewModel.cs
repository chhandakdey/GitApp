using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Models
{
    public class GitAppInputViewModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Repository Url is required")]
        [DisplayName("Re[ository Url")]
        public string RepoUrl { get; set; }
        [DisplayName("Access Token")]
        [Required(ErrorMessage = "Access Token is required")]
        public string AccessToken { get; set; }

    }
}
