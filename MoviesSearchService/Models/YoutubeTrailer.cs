
namespace MoviesSearchService.Models
{
    public class YoutubeTrailer
    {
        public string VideoId { get; set; }
        public string PublishedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public YoutubeTrailerThumbnail Thumbnails { get; set; }

        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
    }

    public class YoutubeTrailerThumbnail
    {
        public YoutubeTrailerThumbnailSize Default;
        public YoutubeTrailerThumbnailSize Medium;
        public YoutubeTrailerThumbnailSize High;
    }

    public class YoutubeTrailerThumbnailSize
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
