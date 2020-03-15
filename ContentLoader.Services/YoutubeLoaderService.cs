using AutoMapper;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Services;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace ContentLoader.Services
{
    public class YoutubeLoaderService : IContentLoaderService
    {
        private readonly IMapper _mapper;

        public YoutubeLoaderService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<byte[]> DownloadVideoByUrlAsync(string url)
        {
            var client = new YoutubeClient();

            var id = YoutubeClient.ParseVideoId(url);

            var test = await client.GetVideoAsync(id);

            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);

            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            var stream = await client.GetMediaStreamAsync(streamInfo);

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public async Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var client = new YoutubeClient();

            var videoId = YoutubeClient.ParseVideoId(url);

            var video = await client.GetVideoAsync(videoId);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videoId);

            var videoInfo = _mapper.Map<VideoContentInfoDto>(video);
            _mapper.Map(streamInfoSet, videoInfo);

            return videoInfo;
        }
    }
}
