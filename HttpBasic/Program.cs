using HttpBasic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<GitHubBranch>();
var app = builder.Build();

app.MapGet("/", async () =>
{
    // var host = new HostBuilder()
    //     .ConfigureServices(svcs =>
    //     {
    //         svcs.AddHttpClient();
    //         svcs.AddTransient<GitHubBranch>();
    //     })
    //     .Build();

    try
    {
        var gitHubService = host.Services.GetRequiredService<GitHubService>();
        var gitHubBranches = await gitHubService.GetAspNetCoreDocsBranchesAsync();
        
        Console.WriteLine($"{gitHubBranches?.Count() ?? 0} GitHub Branches");

        if (gitHubBranches is not null)
        {
            foreach (var gitHubBranch in gitHubBranches)
            {
                Console.WriteLine($"- {gitHubBranch.Name}");
            }
        }
    }
    catch (Exception ex)
    {
        host.Services.GetRequiredService<ILogger<Program>>()
            .LogError(ex, "Unable to load branches from GitHub");
    }
});

app.Run();

