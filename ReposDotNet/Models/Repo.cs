namespace ReposDotNet.Models
{
    public class Repo
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public string URL { get; set; } = string.Empty;
    }
}
