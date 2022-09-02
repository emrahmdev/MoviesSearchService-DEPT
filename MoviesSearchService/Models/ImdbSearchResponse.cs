namespace MoviesSearchService.Models
{
    public class ImdbSearchResponse
    {
        public string ErrorMessage { get; set; }
        public List<Movie> Results { get; set; }
    }
}
