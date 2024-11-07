using Hangfire.Server;

namespace Hangfire.Jobs;

public interface IJob
{
    Task Run(PerformContext performContext, CancellationToken ct);
}