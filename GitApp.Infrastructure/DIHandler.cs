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
        }
    }
}
