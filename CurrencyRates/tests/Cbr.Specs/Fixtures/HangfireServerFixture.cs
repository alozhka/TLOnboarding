using Hangfire;
using Hangfire.InMemory;

namespace Cbr.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    public BackgroundJobServer JobServer { get; }
    public JobStorage JobStorage { get; }

    public HangfireServerFixture()
    {
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        JobStorage = new InMemoryStorage();
        JobServer = new BackgroundJobServer();
        
        
    }
    public void Dispose()
    {
        JobServer.Dispose();
    }
}