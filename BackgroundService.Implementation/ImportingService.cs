namespace BackgroundService.Implementation
{
    public class ImportingService:IImportingService
    {
        public Task Import(string source, int count)
        {
            return Task.FromResult("");
        }
    }
}
