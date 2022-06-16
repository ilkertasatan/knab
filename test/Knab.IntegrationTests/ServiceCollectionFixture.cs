using Microsoft.Extensions.DependencyInjection;

namespace Knab.IntegrationTests;

public class ServiceCollectionFixture
{
    public ServiceCollectionFixture()
    {
        Services = new ServiceCollection();
    }

    public ServiceCollection Services { get; }
}