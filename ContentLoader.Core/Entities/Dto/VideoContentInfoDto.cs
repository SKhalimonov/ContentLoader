using System.Collections.Generic;

namespace ContentLoader.Core.Entities.Dto
{
    public class VideoContentInfoDto : ContentInfoDto
    {
        public string PreviewImageUrl { get; set; }

        public List<DownloadMediaDto> DownloadVideoUrls { get; set; } = new List<DownloadMediaDto>();
    }
}
