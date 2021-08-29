using GitApp.Application.Interfaces;
using GitApp.Application.Services;
using GitApp.Domain;
using GitApp.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application
{
    /// <summary>
    /// DI handler for Application layer
    /// </summary>
    public static class DIHandler
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IGitService, GitService>();
            services.AddTransient<IAsciiSortingService, AsciiSortingService>();            
        }
    }
}
