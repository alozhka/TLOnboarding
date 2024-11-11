using Hangfire.Storage;

namespace HangfireServer.Specs.Drivers;

public class RecurringJobStatus(Dictionary<string, string> entries)
{
    public string LastJobId = entries["lastJobId"];

    public static RecurringJobStatus Fetch(IStorageConnection connection, string recurringJobId)
    {
        Dictionary<string, string> entries = connection.GetAllEntriesFromHash("recurring-job:" + recurringJobId);
        if (entries == null || entries.Count == 0)
        {
            throw new InvalidOperationException("Cannot find recurring job data with ID = " + recurringJobId);
        }

        return new RecurringJobStatus(entries);
    }
}