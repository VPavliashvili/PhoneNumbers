using Domain.Configuration;
using Microsoft.Extensions.Options;

namespace Api.Configuration;

public class ConnectionStringsOptionsSetup : IConfigureOptions<ConnectionStrings>
{
    private const string sectionName = nameof(ConnectionStrings);
    private readonly IConfiguration _configuration;

    public ConnectionStringsOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ConnectionStrings options)
    {
        _configuration.GetSection(sectionName).Bind(options);
    }
}