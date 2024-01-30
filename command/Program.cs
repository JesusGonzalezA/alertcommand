using Alert4U;
using Alert4U.options;
using Alert4U.services;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

var services = new ServiceCollection();
var messageOption = new MessageOption();
var startup = new Startup();

startup.ConfigureServices(services);
var serviceProvider = services.BuildServiceProvider();

var messageOptionService = serviceProvider.GetService<MessageOptionService>();
var rootCommand = serviceProvider.GetService<RootCommand>();

rootCommand.SetHandler(async (message) =>
{
    await messageOptionService.Invoke(message);
}, messageOption);
rootCommand.AddOption(messageOption);

await rootCommand.InvokeAsync(args);