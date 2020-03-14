using System.Threading.Tasks;

namespace ContentLoader.Core.Services
{
    public interface IContentLoaderService
    {
        Task<byte[]> DownloadVideoByUrlAsync(string url);
    }
}
