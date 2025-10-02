using Microsoft.Extensions.DependencyInjection;
using ShelfContext.DL.Read.Queries;
using ShelfContext.Domain.Interfaces.Queries;

namespace ShelfContext.DL.Read.Configuration
{
    public static class QueriesConfiguration
    {
        public static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services
                .AddTransient<IResouceAccessibilityChecker, ResouceAccessibilityChecker>()
                .AddTransient<IBookShelvedChecker, BookShelvedChecker>()
                .AddTransient<IShelfNameUniquenessChecker, ShelfNameUniquenessChecker>();

            return services;
        }
    }
}
