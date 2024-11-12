using System.Globalization;

namespace HangfireServer.Jobs.Date;

/// <summary>
/// Выставляет для часов нужное значение
/// </summary>
public static class ClockHelper
{
    public static void SetUtcNow(string formattedDateTime)
    {
        DateTime parsedDate = DateTime.Parse(formattedDateTime, CultureInfo.InvariantCulture);
        if (parsedDate.Kind == DateTimeKind.Unspecified)
        {
            parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
        }
        
        SetUtcNow(parsedDate);
    }

    public static void ResetUtcNow()
    {
        Clock.UtcNowFactory = null;
    }

    private static void SetUtcNow(DateTime dateTime)
    {
        Clock.UtcNowFactory = () => dateTime;
    }
}