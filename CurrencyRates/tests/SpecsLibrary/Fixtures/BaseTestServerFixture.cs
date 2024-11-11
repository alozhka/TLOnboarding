namespace SpecsLibrary.Fixtures;

public class BaseTestServerFixture<T> where T : class
{
    private static readonly Lazy<BaseTestServerFixture<T>> LazyInstance = new(() => new BaseTestServerFixture<T>());
    public BaseTestServerFixture<T> Instance => LazyInstance.Value;

    private readonly IServiceProvider _serviceProvider;
    private bool _isScenarioRunning;

    public HttpClient HttpClient;

    public BaseTestServerFixture()
    {
        CustomWebApplicationFactory<T> factory = new();

        HttpClient = factory.CreateClient();
        _serviceProvider = factory.Services;
    }
}