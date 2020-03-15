using System.Collections.Generic;

namespace ContentLoader.Core.Entities.Dto
{
    public class VideoContentInfoDto : ContentInfoDto
    {
        public string PreviewImageUrl { get; set; }

        public List<DownloadVideoDto> DownloadInfoUrls { get; set; } = new List<DownloadVideoDto>();
    }
}
