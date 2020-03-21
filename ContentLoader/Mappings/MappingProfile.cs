using AutoMapper;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Entities.Enums;
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
                .ForMember(dest => dest.DownloadVideoUrls, opt => opt.MapFrom(src => src.Muxed))
                .ForMember(dest => dest.DownloadAudioUrl, opt => opt.MapFrom(src => src.Audio.WithHighestBitrate()));
            CreateMap<AudioStreamInfo, DownloadMediaDto>()
                .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.MediaTypeLabel, opt => opt.MapFrom(src => MediaTypes.Mp3))
                .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => MediaTypes.Mp3));
            CreateMap<MuxedStreamInfo, DownloadMediaDto>()
                .ForMember(dest => dest.DownloadUrl, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.MediaTypeLabel, opt => opt.MapFrom(src => src.Container))
                .ForMember(dest => dest.MediaType, opt => opt.MapFrom(src => MediaTypes.Mp4))
                .ForMember(dest => dest.QualityLabel, opt => opt.MapFrom(src => src.VideoQualityLabel));
        }
    }
}
