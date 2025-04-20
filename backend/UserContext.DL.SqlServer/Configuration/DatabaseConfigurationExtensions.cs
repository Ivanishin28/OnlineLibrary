using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.DL.SqlServer.Configuration
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection RegisterDatabase(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost;Database=OnlineLibrary;");
            });

            return services;
        }
    }
}
