using MoviesSearchService.Models;

namespace MoviesSearchService.Services
{
    public class ImdbSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly CacheService _cacheService;

        public ImdbSearchService(HttpClient httpClient, CacheService cacheService)
        {
            _httpClient = httpClient;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Movie>?> SearchMovie(string title)
        {
            var listFromCache = await _cacheService.GetAsync<List<Movie>>(title);

            if(listFromCache != null)
            {
                return listFromCache;
            }

            var response = await _httpClient.GetFromJsonAsync<ImdbSearchResponse>(title);

            if (response == null || !string.IsNullOrEmpty(response.ErrorMessage))
            {
                return null;
            }

            if (response.Results.Count == 0)
            {
                return Enumerable.Empty<Movie>();
            }

            await _cacheService.SetAsync(title, response.Results);

            return response.Results;
        }
    }
}
