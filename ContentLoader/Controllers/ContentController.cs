using ContentLoader.Core.Services;
using ContentLoader.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody]UploadVideoModel uploadModel)
        {
            var fileContents = await _contentLoaderService.DownloadVideoByUrlAsync(uploadModel.Url);
            return File(fileContents, "video/mp4");
        }
    }
}
