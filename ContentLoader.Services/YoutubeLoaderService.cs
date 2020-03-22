using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Services;
using System.Threading.Tasks;
using YoutubeExplode;

namespace ContentLoader.Services
{
    public class YoutubeLoaderService : IContentLoaderService
    {
        private readonly IMapper _mapper;

        public string ServiceDomain => ServiceDomains.YouTube;

        public YoutubeLoaderService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var client = new YoutubeClient();

            var videoId = YoutubeClient.ParseVideoId(url);

            var video = await client.GetVideoAsync(videoId);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videoId);

            var videoInfo = _mapper.Map<VideoContentInfoDto>(video);
            _mapper.Map(streamInfoSet, videoInfo);

            //videoInfo.DownloadVideoUrls.ForEach(x => x.DownloadUrl = x.DownloadUrl.Replace("?", $"?title={videoInfo.Name.Replace(" ", "_")}&"));

            return videoInfo;
        }
    }
}
