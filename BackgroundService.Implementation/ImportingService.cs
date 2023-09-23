using BackgroundService.Implementation.Settings;

namespace BackgroundService.Implementation
{
    public class ImportingService:IImportingService
    {
        public Task Import(ImportingSettings settings)
        {
            return Task.FromResult("");
        }
    }
}
