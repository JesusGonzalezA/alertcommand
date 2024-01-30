using Alert4U.clients;
using Alert4U.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Reflection;

namespace Alert4U;

internal class Startup
{
	IConfiguration _configuration { get; } 

	public Startup()
	{
        string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var builder = new ConfigurationBuilder()
			.SetBasePath(assemblyFolder)
            .AddJsonFile("appsettings.json");

        _configuration = builder.Build();
    }

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton(_configuration);
		services.AddScoped<RootCommand>();
		services.AddHttpClient<MicrosoftTeamsBotClient>(client =>
		{
			client.BaseAddress = new Uri(_configuration.GetValue<string>("BaseUrl"));
		});
        services.AddTransient<MessageOptionService>();
	}
}
