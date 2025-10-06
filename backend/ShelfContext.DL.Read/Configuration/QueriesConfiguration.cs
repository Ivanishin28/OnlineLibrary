using Microsoft.Extensions.DependencyInjection;
using ShelfContext.Contract.Services;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Configuration
{
    public static class QueriesConfiguration
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services
                .AddTransient<ITagNameUniquenessChecker, TagNameUniquenessChecker>()
                .AddTransient<IResouceAccessibilityChecker, ResouceAccessibilityChecker>()
                .AddTransient<IShelfNameUniquenessChecker, ShelfNameUniquenessChecker>();

            return services;
        }
    }
}
