using Hangfire;
using Hangfire.Storage;

namespace Cbr.Specs.Drivers;

public class HangfireTestDriver : IDisposable
{
    private readonly Lazy<IStorageConnection> _storageConnection = new(GetStorageConnection);

    private static IStorageConnection GetStorageConnection()
    {
        return JobStorage.Current.GetConnection() ??
               throw new InvalidOperationException("Hangfire test database connection is null");
    }
    public void Dispose()
    {
        // TODO release managed resources here
    }
}