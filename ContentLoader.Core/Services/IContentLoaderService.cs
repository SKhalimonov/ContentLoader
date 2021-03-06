﻿using ContentLoader.Core.Entities.Dto;
using System.Threading.Tasks;

namespace ContentLoader.Core.Services
{
    public interface IContentLoaderService
    {
        string ServiceDomain { get; }

        Task<VideoContentInfoDto> GetVideoInfoAsync(string url);
    }
}
