using DL.Interfaces;

namespace BackendMedia
{
    public class Application
    {
        private IDbInitializer _dbInitializer;

        public Application(IDbInitializer dbInitializer)
        {
            _dbInitializer = dbInitializer;
        }

        public async Task Start()
        {
            await _dbInitializer.Initialize();
        }
    }
}
