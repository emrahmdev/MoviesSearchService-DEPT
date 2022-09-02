using Microsoft.AspNetCore.Mvc;
using MoviesSearchService.Models;
using MoviesSearchService.Services;

namespace MoviesSearchService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MoviesController : Controller
    {
        private readonly ImdbSearchService _imdbSearchService;
        private readonly YouTubeTrailerSearchService _youTubeTrailerSearchService;
        private readonly CacheService _cacheService;

        public MoviesController(ImdbSearchService imdbSearchService, YouTubeTrailerSearchService youTubeTrailerSearchService, CacheService cacheService)
        {
            _imdbSearchService = imdbSearchService;
            _youTubeTrailerSearchService = youTubeTrailerSearchService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string title)
        {
            var searchTerm = title.Trim();

            var listOfMovies = await _imdbSearchService.SearchMovie(searchTerm);

            if(listOfMovies == null)
            {
                return StatusCode(500);
            }

            if (listOfMovies.Count() == 0)
            {
                return StatusCode(404);
            }

            foreach (var movie in listOfMovies)
            {
                var listFromCache = await _cacheService.GetAsync<List<YoutubeTrailer>>(movie.Id);

                if(listFromCache != null)
                {
                    movie.YoutubeTrailerList = listFromCache;
                    continue;
                }

                var listOfYoutubeVideos = await _youTubeTrailerSearchService.SearchTrailer(movie.Title);
                movie.YoutubeTrailerList = listOfYoutubeVideos;

                if (listOfYoutubeVideos.Count() == 0) continue;

                await _cacheService.SetAsync(movie.Id, listOfYoutubeVideos);
            }

            return Ok(listOfMovies);

        }
    }
}
