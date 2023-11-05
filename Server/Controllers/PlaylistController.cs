using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoNestServer.JsonStructures;
using VideoNestServer.Model;
using VideoNestServer.Utilities;

namespace VideoNestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        [HttpGet]
        [Route("single/{guid}")]
        [Authorize]
        public IActionResult GetSinglePlaylist(string guid)
        {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            Guid playlistGuid = new Guid(guid);

            using (VideonestContext context = new VideonestContext())
            {
                Playlist? playlist = (from playlists in context.Playlists
                                     join playlistAccess1 in context.PlaylistAccesses on playlists.Guid equals playlistAccess1.Playlistguid
                                     where playlistAccess1.Accountguid == userGuid
                                     select new Playlist
                                     {
                                         Guid = playlists.Guid,
                                         Description = playlists.Description,
                                         Name = playlists.Name,
                                         Type = playlists.Type,
                                     }).SingleOrDefault();

                if (playlist == null)
                {
                    return Forbid();
                }

                PlaylistAccess? playlistAccess = (from playlistAccess2 in context.PlaylistAccesses
                                                  where playlistAccess2.Playlistguid == playlist.Guid
                                                  && playlistAccess2.Accountguid == userGuid
                                                  select new PlaylistAccess
                                                  {
                                                      Playlistguid = playlistAccess2.Playlistguid,
                                                      Accountguid = playlistAccess2.Accountguid,
                                                      Accesslevel = playlistAccess2.Accesslevel
                                                  }).SingleOrDefault();

                List<Video> videoList = (from videos in context.Videos
                 join playlistVideos in context.PlaylistVideos on videos.Guid equals playlistVideos.Videoguid
                 join playlists in context.Playlists on playlistVideos.Playlistguid equals playlists.Guid
                 join playlistAccess3 in context.PlaylistAccesses on playlists.Guid equals playlistAccess3.Playlistguid
                 where playlistAccess3.Accountguid == userGuid
                 select new Video
                 {
                     Guid = videos.Guid,
                     Title = videos.Title,
                     Creator = videos.Creator,
                     Filename = videos.Filename,
                     Sourcelink = videos.Sourcelink,
                     Sourcetype = videos.Sourcetype
                 }).ToList();

                return new JsonResultBuilder()
                    .set("playlist", playlist)
                    .set("playlistAccess", playlistAccess)
                    .set("videos", videoList)
                    .get();
            }
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public IActionResult GetAllAvailablePlaylists()
        {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            using (VideonestContext context = new VideonestContext())
            {
                var resultList = (from playlists in context.Playlists
                 join playlistAccess in context.PlaylistAccesses on playlists.Guid equals playlistAccess.Playlistguid
                 where playlistAccess.Accountguid == userGuid
                 select new Playlist
                 {
                     Guid = playlists.Guid,
                     Name = playlists.Name,
                     Description = playlists.Description,
                     Type = playlists.Type
                 }).ToList();

                return new JsonResultBuilder().set("playlists", resultList).get();
            }
        }

        [HttpGet]
        [Route("count")]
        [Authorize]
        public IActionResult GetMyPlaylistCount() {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            using (VideonestContext context = new VideonestContext())
            {
                int playlistCount = (from playlist in context.Playlists
                 join access in context.PlaylistAccesses on playlist.Guid equals access.Playlistguid
                 where access.Accountguid == userGuid
                 select new Playlist
                 {
                     Guid = playlist.Guid,
                     Name = playlist.Name,
                     Description = playlist.Description,
                     Type = playlist.Type,
                 }).Count();                
                
                return new JsonResultBuilder().set("count", playlistCount).get();
            }
        }
    }
}