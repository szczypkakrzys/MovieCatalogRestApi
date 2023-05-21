namespace Movie_Catalog_REST_API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime WorldPremiereDate { get; set; } 
        public ICollection<string> Genre { get; set; } = new List<string>();
        public ICollection<string> Director { get; set; } = new List<string>();
        public int Duration { get; set; }
        public DateTime TimeCreated { get; } = DateTime.Now;
    }
}
