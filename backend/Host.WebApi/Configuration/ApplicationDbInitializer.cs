using Shared.DL.Interfaces;

namespace Host.WebApi.Configuration
{
    public class ApplicationDbInitializer
    {
        private IServiceProvider _provider;

        public ApplicationDbInitializer(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Initialize()
        {
            using var scope = _provider.CreateScope();
            var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
            
            foreach (var dbInit in initializers)
            {
                await dbInit.Initialize();
            }
        }
    }
}
