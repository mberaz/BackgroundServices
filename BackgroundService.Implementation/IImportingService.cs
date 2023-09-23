using BackgroundService.Implementation.Settings;

namespace BackgroundService.Implementation
{
    public interface IImportingService
    {
        Task Import(ImportingSettings settings);
    }
}
