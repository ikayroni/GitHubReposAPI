using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using GitHubReposAPI.Models;

namespace GitHubReposAPI.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private const string GitHubApiUrl = "https://api.github.com/orgs/takenet/repos";

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubReposAPI");
        }

        public async Task<List<RepoModel>> GetOldestCSharpReposAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<JsonElement>(GitHubApiUrl);
            var repos = response.EnumerateArray()
                .Where(repo => repo.GetProperty("language").GetString() == "C#")
                .OrderBy(repo => DateTime.Parse(repo.GetProperty("created_at").GetString()))
                .Take(5)
                .Select(repo => new RepoModel
                {
                    Title = repo.GetProperty("full_name").GetString(),
                    Subtitle = repo.GetProperty("description").GetString()
                })
                .ToList();

            return repos;
        }
    }
}
