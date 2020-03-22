using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Exceptions;
using ContentLoader.Core.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentLoader.Services
{
    public class MediaService : IMediaService
    {
        private readonly List<IContentLoaderService> _servicesFactory;

        public MediaService(IMapper mapper, Config config)
        {
            _servicesFactory = new List<IContentLoaderService>()
            {
                new YoutubeLoaderService(mapper),
                new InstagramLoaderService(mapper, config),
                new FacebookLoaderService(mapper, config)
            };
        }

        public Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var service = GetServiceByUrl(url);

            return service.GetVideoInfoAsync(url);
        }

        public async Task<byte[]> DownloadVideoAsync(string playerUrl)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(playerUrl);

            return await response.Content.ReadAsByteArrayAsync();
        }

        private IContentLoaderService GetServiceByUrl(string url)
        {
            var uri = new Uri(url);

            var service = _servicesFactory.Find(x => x.ServiceDomain == uri.Host.ToLower());
            if (service == null)
            {
                throw new UnsupportedServiceException(uri.Host);
            }

            return service;
        }
    }
}
