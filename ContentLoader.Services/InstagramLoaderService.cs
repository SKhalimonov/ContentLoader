﻿using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Services;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ContentLoader.Services
{
    public class InstagramLoaderService : IContentLoaderService
    {
        private readonly IMapper _mapper;

        private readonly ServiceConfig _serviceConfig;

        private readonly Config _config;

        public string ServiceDomain => ServiceDomains.Instagram;

        public InstagramLoaderService(IMapper mapper, Config config)
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

            var result = new VideoContentInfoDto
            {
                Name = "Instagram Video",
                PreviewImageUrl = videoNode.GetAttribute("poster"),
                DownloadVideoUrls = new List<DownloadMediaDto>()
                {
                    new DownloadMediaDto
                    {
                        MediaTypeLabel = "Mp4",
                        DownloadUrl = videoNode.GetAttribute("src")
                    }
                }
            };

            webDriver.Dispose();

            return result;
        }
    }
}
