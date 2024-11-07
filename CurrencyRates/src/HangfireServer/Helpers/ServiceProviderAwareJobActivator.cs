
using Hangfire;

namespace HangfireServer.Helpers;

/// <summary>
/// Позволяет Hangfire получить зависимости из DI контейнера
/// </summary>
/// <param name="serviceProvider"></param>
public class ServiceProviderAwareJobActivator(IServiceProvider serviceProvider) : JobActivator
{
    public override object ActivateJob(Type jobType)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, jobType);
    }

    public override JobActivatorScope BeginScope(JobActivatorContext context)
    {
        return new ServiceProviderAwareJobActivatorScope(serviceProvider.CreateScope());
    }

    private class ServiceProviderAwareJobActivatorScope(IServiceScope scope) : JobActivatorScope
    {
        public override object Resolve(Type type)
        {
            return ActivatorUtilities.CreateInstance(scope.ServiceProvider, type);
        }

        public override void DisposeScope()
        {
            scope.Dispose();
        }
    }
}
