using ContentLoader.Core.Entities.Dto;
using System.Threading.Tasks;

namespace ContentLoader.Core.Services
{
    public interface IContentLoaderService
    {
        Task<byte[]> DownloadVideoByUrlAsync(string url);

        Task<VideoContentInfoDto> GetVideoInfoAsync(string url);
    }
}
