using System.Text.Json;

namespace HttpBasic;

public class GitHubService
{
    public readonly IHttpClientFactory _httpClientFactory;

    public GitHubService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<GitHubBranch>?> GetAspNetCoreDocsBranchesAsync()
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
        {
            Headers =
            {
                {"Accept", "application/vnd.github.v3+json"},
                {"User-Agent", "HttpRequestsConsoleSample"}
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
    }
}