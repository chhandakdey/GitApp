using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Infrastructure.Adapters;
using GitApp.Infrastructure.Adapters.Interfaces;
using GitApp.Infrastructure.Daos;
using GitApp.Infrastructure.Repositories;
using GitApp.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Infrastructure
{
    public static class DIHandler
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IGitRepository, GitRepository>();
            services.AddScoped<IDao<GitRequestDTO, IEnumerable<GitCommentDTO>>, GitDao>();
            services.AddScoped<IApiClient, GitApiClient>();
        }
    }
}
