using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Services;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentLoader.Services
{
    public class InstagramLoaderService : IContentLoaderService
    {
        private readonly IMapper _mapper;

        private readonly ServiceConfig _config;

        public string ServiceDomain => ServiceDomains.Instagram;

        public InstagramLoaderService(IMapper mapper, Config config)
        {
            _mapper = mapper;
            _config = config.ServicesConfigs[ServiceDomain];
        }

        public async Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var httpClient = new HttpClient();

            var htmlContent = await httpClient.GetStringAsync(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var videoNode = doc.DocumentNode.SelectSingleNode(_config.VideoSelector);

            return new VideoContentInfoDto
            {
                Name = "Instagram Video",
                PreviewImageUrl = videoNode.Attributes["poster"].Value,
                DownloadVideoUrls = new List<DownloadMediaDto>()
                {
                    new DownloadMediaDto
                    {
                        MediaTypeLabel = "Mp4",
                        DownloadUrl = videoNode.Attributes["src"].Value
                    }
                }
            };
        }
    }
}
