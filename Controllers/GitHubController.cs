using GitHubReposAPI.Models;
using GitHubReposAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GitHubReposAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("csharp-repos")]
        public async Task<IActionResult> GetOldestCSharpRepos()
        {
            try
            {
                var repos = await _gitHubService.GetOldestCSharpReposAsync();
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar repositórios: {ex.Message}");
            }
        }
    }
}
