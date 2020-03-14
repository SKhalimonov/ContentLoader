using ContentLoader.Core.Entities.Enums;

namespace ContentLoader.Model
{
    public class UploadVideoModel
    {
        public string Url { get; set; }

        public VideoServiceTypes VideoService { get; set; }
    }
}
