using Microsoft.AspNetCore.Mvc.Testing;

namespace Cbr.Specs.Fixtures;

public class HangfireServerFixture : IDisposable
{
    public HangfireServerFixture()
    {
        WebApplicationFactory<HangfireProgram> factory = new();
    }
    public void Dispose()
    {
    }
}