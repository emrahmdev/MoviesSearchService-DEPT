using AutoMapper;
using Google.Apis.YouTube.v3;
using MoviesSearchService.Models;

namespace MoviesSearchService.Services
{
    public class YouTubeTrailerSearchService
    {
        private readonly YouTubeService _youTubeService;
        private readonly IMapper _mapper;

        public YouTubeTrailerSearchService(YouTubeService youTubeService, IMapper mapper) { 
            _youTubeService = youTubeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<YoutubeTrailer>> SearchTrailer(string title)
        {
            var searchRequest = _youTubeService.Search.List("id,snippet");
            searchRequest.Q = $"{title} Trailer";
            searchRequest.TopicId = "/m/02vxn";

            var searchListResponse = await searchRequest.ExecuteAsync();

            if(searchListResponse == null)
            {
                return Enumerable.Empty<YoutubeTrailer>();
            }

            var listOfVideos = searchListResponse.Items.Select(x => _mapper.Map<YoutubeTrailer>(x));

            return listOfVideos;
        }
    }
}
