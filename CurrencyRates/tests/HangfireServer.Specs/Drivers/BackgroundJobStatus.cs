using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace HangfireServer.Specs.Drivers;

public class BackgroundJobStatus(StateData data)
{
    private static readonly string[] FinalStateNames = [SucceededState.StateName, DeletedState.StateName];
    private const string ExceptionKey = "Exception";

    public bool IsFinal => FinalStateNames.Contains(data.Name);
    public bool IsSucceeded => SucceededState.StateName == data.Name;

    public Exception LoadException()
    {
        if (data.Data.TryGetValue(ExceptionKey, out string? serializedException))
        {
            ExceptionInfo exceptionInfo = SerializationHelper.Deserialize<ExceptionInfo>(serializedException);
            return CreateException(exceptionInfo);
        }

        return new InvalidOperationException("State has no exception data");
    }

    private static Exception CreateException(ExceptionInfo info)
    {
        Exception? innerException = info.InnerException != null ? CreateException(info.InnerException) : null;
        Type type = TypeHelper.CurrentTypeResolver(info.Type);
        object[] args = innerException != null ? [info.Message, innerException] : [info.Message];
        object? instance;

        try
        {
            instance = Activator.CreateInstance(type, args);
        }
        catch (MissingMethodException)
        {
            instance = Activator.CreateInstance(typeof(InvalidOperationException), args);
        }

        if (instance == null)
        {
            throw new InvalidOperationException($"Cannot create exception of type {type} with message {info.Message}");
        }

        return (Exception)instance;
    }
    
    public static BackgroundJobStatus Fetch(IStorageConnection connection, string jobId)
    {
        StateData? data = connection.GetStateData(jobId);
        if (data == null)
        {
            throw new InvalidOperationException($"Cannot find job with id='{jobId}'");
        }

        return new BackgroundJobStatus(data);
    }
}