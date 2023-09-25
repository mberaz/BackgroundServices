using BackgroundService.Implementation.Settings;

namespace BackgroundServices.BackgroundModels;

public class TaskSettings
{
    public MonitorSettings? MonitorSettings { get; set; }
    public ImportingSettings? ImportingSettings { get; set; }

    public static TaskSettings FromMonitorSettings(MonitorSettings monitorSettings)
    {
        return new TaskSettings { MonitorSettings = monitorSettings };
    }

    public static TaskSettings FromImporterSettings(ImportingSettings importingSettings)
    {
        return new TaskSettings { ImportingSettings = importingSettings };
    }
}