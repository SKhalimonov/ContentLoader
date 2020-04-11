using ContentLoader.Core.Configurations;
using ContentLoader.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ContentLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ContentLoaderOrigins")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        private readonly ILogger<MediaController> _logger;

        private readonly Config _config;

        public MediaController(IMediaService mediaService, ILogger<MediaController> logger, Config config)
        {
            _mediaService = mediaService;
            _logger = logger;
            _config = config;
        }

        [HttpGet("info")]
        public async Task<object> GetInfoAsync([FromQuery]string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Url is required");
            }

            _logger.LogInformation(_config.GetBrowserEnginePath());

            try
            {
                var contentInfo = await _mediaService.GetVideoInfoAsync(url);
                return Ok(contentInfo);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred GET 'media/info'");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("download")]
        public async Task<object> DownloadVideoAsync([FromQuery]string playerUrl)
        {
            if (string.IsNullOrEmpty(playerUrl))
            {
                return BadRequest("PlayerUrl is required");
            }

            try
            {
                var contentInfo = await _mediaService.DownloadVideoAsync(playerUrl);
                return File(contentInfo, "video/mp4");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred GET 'media/download'");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
