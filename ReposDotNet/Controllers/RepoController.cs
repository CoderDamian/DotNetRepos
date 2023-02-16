using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReposDotNet.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ReposDotNet.Controllers
{
    public class RepoController : Controller
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _options;

        public RepoController(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            _httpClient.BaseAddress = new Uri("https://api.github.com/orgs/dotnet/repos");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index(string language)
        {
            var response = await _httpClient.GetAsync("").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                IEnumerable<Repo>? repos = await JsonSerializer.DeserializeAsync<IEnumerable<Repo>>(content, _options).ConfigureAwait(false);

                if (repos != null)
                {
                    ReposViewModel reposVM = new ReposViewModel()
                    {
                        Languages = new SelectList(repos.Select(r => r.Language).Distinct().ToList())
                    };

                    if (!String.IsNullOrWhiteSpace(language))
                        repos = repos.Where(r => r.Language != null && r.Language.Contains(language)).ToList();

                    reposVM.Repos = repos.ToList();

                    return View(reposVM);
                }
                else
                {
                    return Ok(default(IEnumerable<Repo>));
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
