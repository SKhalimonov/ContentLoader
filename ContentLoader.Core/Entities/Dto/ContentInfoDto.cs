﻿using System.Collections.Generic;

namespace ContentLoader.Core.Entities.Dto
{
    public class ContentInfoDto
    {
        public int Duration { get; set; }

        public string Name { get; set; }

        public DownloadMediaDto DownloadAudioUrl { get; set; } = new DownloadMediaDto();
    }
}
