namespace HangfireServer.Helpers.Date;

public static class Clock
{
    public static Func<DateTime>? UtcNowFactory { private get; set; }

    public static DateTime UtcNow()
    {
        return UtcNowFactory?.Invoke() ?? DateTime.UtcNow;
    }
}