using ContentLoader.Core.Services;
using ContentLoader.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContentLoader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class ContentController : ControllerBase
    {
        private readonly IContentLoaderService _contentLoaderService;

        public ContentController(IContentLoaderService contentLoaderService)
        {
            _contentLoaderService = contentLoaderService;
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
                var contentInfo = await _contentLoaderService.GetVideoInfoAsync(url);
                return Ok(contentInfo);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("download")]
        public async Task<object> DownloadAsync([FromBody]UploadVideoModel model)
        {
            if (model == null)
            {
                return BadRequest("model is required");
            }

            try
            {
                var contentInfo = await _contentLoaderService.GetVideoInfoAsync(url);
                return Ok(contentInfo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
