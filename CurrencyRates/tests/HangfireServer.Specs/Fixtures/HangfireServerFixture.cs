using Microsoft.AspNetCore.Mvc.Testing;

namespace HangfireServer.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    public HangfireServerFixture()
    {
        WebApplicationFactory<Program> factory = new();
    }
    public void Dispose()
    {
    }
}