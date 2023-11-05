using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoNestServer.Model;
using VideoNestServer.Utilities;

namespace VideoNestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        [HttpGet]
        [Route("file/{fileGuid}")]
        public IActionResult GetVideo(string fileGuid)
        {
            string fileSavePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\Videos";
            using (VideonestContext context = new VideonestContext())
            {
                Video? videoRecord = context.Videos.SingleOrDefault(x => x.Guid == new Guid(fileGuid));

                if (videoRecord == null)
                {
                    return NotFound();
                }

                var videoPath = Path.Combine(fileSavePath, videoRecord.Filename ?? "NOT_FOUND.MP4");

                if (System.IO.File.Exists(videoPath))
                {
                    var fileStream = System.IO.File.OpenRead(videoPath);
                    return File(fileStream, contentType: $"video/{videoRecord.Filename?.Split('.')[1] ?? "mp4"}", enableRangeProcessing: true);
                }
            }

            return NotFound();
        }

        [HttpGet]
        [Route("thumbnail/{fileGuid}")]
        public IActionResult GetThumbnail(string fileGuid)
        {
            string fileSavePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\Videos";

            try
            {
                var imagePath = Path.Combine(fileSavePath, fileGuid + ".webp");

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Image not found");
                }

                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/webp");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("counts")]
        [Authorize]
        public IActionResult GetCounts()
        {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            using (VideonestContext context = new VideonestContext())
            {
                List<Video> videoList = (from videos in context.Videos
                 join playlistVideos in context.PlaylistVideos on videos.Guid equals playlistVideos.Videoguid
                 join playlists in context.Playlists on playlistVideos.Playlistguid equals playlists.Guid
                 join playlistAccess in context.PlaylistAccesses on playlists.Guid equals playlistAccess.Playlistguid
                 join accounts in context.Accounts on playlistAccess.Accountguid equals accounts.Guid
                 where accounts.Guid == userGuid
                 select new Video
                 {
                     Guid = videos.Guid,
                     Sourcetype = videos.Sourcetype
                 }).ToList();

                return new JsonResultBuilder()
                    .set("youtube", videoList.Count(x => x.Sourcetype == (int)Enum.SourceTypeDef.YouTube))
                    .set("tiktok", videoList.Count(x => x.Sourcetype == (int)Enum.SourceTypeDef.TikTok)).get();
            }
        }
    }
}