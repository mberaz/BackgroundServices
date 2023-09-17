using BackgroundServices.BackgroundModels.TasksSettings;

namespace BackgroundServices.BackgroundModels;

public class HostingSettings
{
    public MonitorSettings? MonitorSettings { get; set; }
    public ImportingSettings? ImportingSettings { get; set; }

    public static HostingSettings FromMonitorSettings(MonitorSettings monitorSettings)
    {
        return new HostingSettings { MonitorSettings = monitorSettings };
    }

    public static HostingSettings FromImporterSettings(ImportingSettings importingSettings)
    {
        return new HostingSettings { ImportingSettings = importingSettings };
    }
}