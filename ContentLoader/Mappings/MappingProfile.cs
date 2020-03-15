using AutoMapper;
using ContentLoader.Core.Entities.Dto;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

namespace ContentLoader.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapYouTube();
        }

        private void MapYouTube()
        {
            CreateMap<Video, VideoContentInfoDto>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.TotalSeconds))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PreviewImageUrl, opt => opt.MapFrom(src => src.Thumbnails.MaxResUrl));
            CreateMap<MediaStreamInfoSet, VideoContentInfoDto>()
                .ForMember(dest => dest.DownloadInfoUrls, opt => opt.MapFrom(src => src.Muxed));
            CreateMap<MuxedStreamInfo, DownloadVideoDto>()
                .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.FormatLabel, opt => opt.MapFrom(src => src.Container))
                .ForMember(dest => dest.QualityLabel, opt => opt.MapFrom(src => src.VideoQualityLabel));
        }
    }
}
