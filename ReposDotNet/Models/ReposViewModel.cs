using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReposDotNet.Models
{
    public class ReposViewModel
    {
        public SelectList? Languages { get; set; }
        public List<Repo>? Repos { get; set; }
        public string Language { get; set; } = string.Empty;
        public byte Page { get; set; }
        public byte Size { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }

    }
}
