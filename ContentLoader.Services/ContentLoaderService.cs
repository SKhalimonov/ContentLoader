using ContentLoader.Core.Services;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace ContentLoader.Services
{
    public class ContentLoaderService : IContentLoaderService
    {
        public async Task<byte[]> DownloadVideoByUrlAsync(string url)
        {
            var client = new YoutubeClient();

            var id = YoutubeClient.ParseVideoId(url);

            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);

            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            var stream = await client.GetMediaStreamAsync(streamInfo);

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
