using ContentLoader.Core.Entities.Enums;

namespace ContentLoader.Core.Entities.Dto
{
    public class DownloadMediaDto
    {
        public string DownloadUrl { get; set; }

        public string QualityLabel { get; set; }

        public string MediaTypeLabel { get; set; }

        public MediaTypes MediaType { get; set; }
    }
}
