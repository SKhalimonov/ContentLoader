using AutoMapper;
using ContentLoader.Core.Configurations;
using ContentLoader.Core.Entities.Dto;
using ContentLoader.Core.Exceptions;
using ContentLoader.Core.Services;
using System;
using System.Collections.Generic;
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
                new InstagramLoaderService(mapper, config)
            };
        }

        public Task<VideoContentInfoDto> GetVideoInfoAsync(string url)
        {
            var service = GetServiceByUrl(url);

            return service.GetVideoInfoAsync(url);
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
