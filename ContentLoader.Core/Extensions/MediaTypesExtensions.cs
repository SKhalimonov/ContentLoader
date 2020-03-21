using ContentLoader.Core.Entities.Enums;

namespace ContentLoader.Core.Extensions
{
    public static class MediaTypesExtensions
    {
        public static string GetMediaTypeName(this MediaTypes mediaType)
        {
            switch(mediaType)
            {
                case MediaTypes.Mp3:
                    return "audio/mpeg";
                case MediaTypes.Mp4:
                    return "video/mp4";
            }
            return "application/octet-stream";
        }
    }
}
