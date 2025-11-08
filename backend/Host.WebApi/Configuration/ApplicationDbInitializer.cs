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
            var bookDbInit = scope.ServiceProvider.GetRequiredService<BookContext.DL.SqlServer.Interfaces.IDbInitializer>();
            await bookDbInit.Initialize();
        }
    }
}
