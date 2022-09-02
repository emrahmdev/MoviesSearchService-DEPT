namespace MoviesSearchService.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<YoutubeTrailer> YoutubeTrailerList { get; set; }
    }
}
