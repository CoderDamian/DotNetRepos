using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReposDotNet.Models
{
    public class ReposViewModel
    {
        public SelectList? Languages { get; set; }
        public List<Repo>? Repos { get; set; }
        public string Language { get; set; } = string.Empty;
    }
}
