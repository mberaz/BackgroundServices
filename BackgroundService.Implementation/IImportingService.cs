namespace BackgroundService.Implementation
{
    public interface IImportingService
    {
        Task Import(string source,int count);
    }
}
