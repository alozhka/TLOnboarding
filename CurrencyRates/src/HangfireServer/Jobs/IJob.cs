using Hangfire.Server;

namespace HangfireServer.Jobs;

public interface IJob
{
    Task Run(PerformContext performContext, CancellationToken ct);
}