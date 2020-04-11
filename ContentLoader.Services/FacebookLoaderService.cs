using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Services;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentLoader.Services
{
    public class FacebookLoaderService : IContentLoaderService
    {
        private readonly IMapper _mapper;

        private readonly ServiceConfig _serviceConfig;

        private readonly Config _config;

        public string ServiceDomain => ServiceDomains.Facebook;

        public FacebookLoaderService(IMapper mapper, Config config)
        {
            _mapper = mapper;
            _config = config;
            _serviceConfig = config.ServicesConfigs[ServiceDomain];
        }

        public async Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var webDriver = new ChromeDriver(_config.ContentRootPath);
            webDriver.Url = url;

            var videoNode = webDriver.FindElementByXPath(_serviceConfig.VideoSelector);
            var titleNode = webDriver.FindElementByXPath(_serviceConfig.TitleSelector);
            var imageNode = webDriver.FindElementByXPath(_serviceConfig.ImageSelector);

            var result = new VideoContentInfoDto
            {
                Name = titleNode.GetAttribute("content"),
                PreviewImageUrl = imageNode.GetAttribute("content"),
                DownloadVideoUrls = new List<DownloadMediaDto>()
                {
                    new DownloadMediaDto
                    {
                        MediaTypeLabel = "Mp4",
                        DownloadUrl = videoNode.GetAttribute("content")
                    }
                }
            };

            webDriver.Dispose();

            return result;
        }
    }
}
