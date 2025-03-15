namespace BitrateUtility.Source;

static class Program
{
    private static UniFiTrafficMonitorApplication UniFiTrafficMonitorApplication { get; } = new();
    
    public static async Task Main()
    {
        await UniFiTrafficMonitorApplication.Run();
    }
}