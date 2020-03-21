using ContentLoader.Core.Entities.Dto;
using System.Threading.Tasks;

namespace ContentLoader.Core.Services
{
    public interface IMediaService
    {
        Task<VideoContentInfoDto> GetVideoInfoAsync(string url);
    }
}
