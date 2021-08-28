using GitApp.Application.Interfaces;
using GitApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application
{
    public static class DIHandler
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGitService, GitService>();
        }
    }
}
