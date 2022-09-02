using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using MoviesSearchService.Models;

namespace MoviesSearchService.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Thumbnail, YoutubeTrailerThumbnailSize>();
            CreateMap<ThumbnailDetails, YoutubeTrailerThumbnail>()
                .ForMember(dest => dest.Default, act => act.MapFrom(opt => opt.Default__));

            CreateMap<SearchResult, YoutubeTrailer>()
                .ForMember(dest => dest.VideoId, act => act.MapFrom(src => src.Id.VideoId))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Snippet.Title))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Snippet.Description))
                .ForMember(dest => dest.PublishedAt, act => act.MapFrom(src => src.Snippet.PublishedAt))
                .ForMember(dest => dest.ChannelTitle, act => act.MapFrom(src => src.Snippet.ChannelTitle))
                .ForMember(dest => dest.ChannelId, act => act.MapFrom(src => src.Snippet.ChannelId))
                .ForMember(dest => dest.Thumbnails, act => act.MapFrom(src => src.Snippet.Thumbnails));
        }
    }
}
