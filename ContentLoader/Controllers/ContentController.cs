using ContentLoader.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContentLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet("info")]
        public async Task<object> GetInfoAsync([FromQuery]string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("Url is required");
            }

            try
            {
                var contentInfo = await _mediaService.GetVideoInfoAsync(url);
                return Ok(contentInfo);
            }
            catch(Exception ex)
            {
                return BadRequest();
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
                return BadRequest();
            }
        }
    }
}
