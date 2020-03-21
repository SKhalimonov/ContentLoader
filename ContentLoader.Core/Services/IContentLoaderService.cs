using ContentLoader.Core.Entities.Dto;
using System.Threading.Tasks;

namespace ContentLoader.Core.Services
{
    public interface IContentLoaderService
    {
        Task<VideoContentInfoDto> GetVideoInfoAsync(string url);

        Task<byte[]> DownloadFileAsync(string fileUrl);
    }
}
