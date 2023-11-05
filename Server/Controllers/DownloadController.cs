using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using VideoNestServer.JsonStructures;
using VideoNestServer.Model;
using VideoNestServer.Utilities;

namespace VideoNestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public IActionResult Download([FromBody] DownloadRequest downloadRequest)
        {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            Guid videoGuid = Guid.NewGuid();

            string videoTitle = "";
            string videoChannel = "";
            int platform = (int) (downloadRequest.url.ToLower().Contains("tiktok") ? Enum.SourceTypeDef.TikTok : Enum.SourceTypeDef.YouTube);

            string executablePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\ThirdParty\yt-dlp.exe";
            string fileSavePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\Videos";

            if (!Directory.Exists(fileSavePath)) {
                Directory.CreateDirectory(fileSavePath);
            }

            Process titleProcess = new Process();
            titleProcess.StartInfo.FileName = executablePath;
            titleProcess.StartInfo.Arguments = $"--print \"%(channel)s[separator$$#]%(title)s\" {downloadRequest.url}";
            titleProcess.StartInfo.RedirectStandardOutput = true;
            titleProcess.StartInfo.CreateNoWindow = false;
            titleProcess.StartInfo.UseShellExecute = false;
            titleProcess.StartInfo.RedirectStandardOutput = true;
            titleProcess.StartInfo.RedirectStandardError = true;

            titleProcess.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    videoTitle = e.Data.Split("[separator$$#]", StringSplitOptions.None)[1];
                    videoChannel = e.Data.Split("[separator$$#]", StringSplitOptions.None)[0].Replace(" ", "_");
                    if (videoChannel == "NA" && downloadRequest.url.ToLower().Contains("tiktok"))
                    {
                        //TikTok Channel Split

                        string[] splitOne = downloadRequest.url.Split('@');

                        if (splitOne.Length > 1)
                        {
                            videoChannel = splitOne[1].Split("/")[0];
                        }
                    }
                }
            };

            titleProcess.Start();
            titleProcess.BeginOutputReadLine();

            titleProcess.WaitForExit();

            Process downloaderProcess = new Process();
            downloaderProcess.StartInfo.FileName = executablePath;
            downloaderProcess.StartInfo.Arguments = $"-S vcodec:h264 --write-thumbnail --recode mp4 -o \"{videoGuid}.%(ext)s\" -P \"{fileSavePath}\" {downloadRequest.url}";
            downloaderProcess.StartInfo.RedirectStandardOutput = true;
            downloaderProcess.StartInfo.CreateNoWindow = false;
            downloaderProcess.StartInfo.UseShellExecute = false;
            downloaderProcess.StartInfo.RedirectStandardOutput = true;
            downloaderProcess.StartInfo.RedirectStandardError = true;

            downloaderProcess.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine(e.Data);
                }
            };

            downloaderProcess.Start();
            downloaderProcess.BeginOutputReadLine();

            downloaderProcess.WaitForExit();

            using (VideonestContext context = new VideonestContext())
            {
                context.Videos.Add(new Video()
                {
                    Guid = videoGuid,
                    Sourcelink = downloadRequest.url,
                    Sourcetype = platform,
                    Filename = $"{videoGuid}.mp4",
                    Title = videoTitle,
                    Creator = videoChannel
                });

                Playlist? usersMainPlaylist = (from playlists in context.Playlists
                 join playlistAccess in context.PlaylistAccesses on playlists.Guid equals playlistAccess.Playlistguid
                 where playlistAccess.Accountguid == userGuid
                     && playlists.Type == (int)Enum.PlaylistTypeDef.Main
                     && playlistAccess.Accesslevel == (int)Enum.PlaylistAccessLevelDef.Owner
                 select new Playlist
                 {
                     Guid = playlists.Guid,
                     Name = playlists.Name,
                     Description = playlists.Description,
                     Type = playlists.Type
                 }).SingleOrDefault();

                if (usersMainPlaylist == null)
                {
                    return Ok();
                }

                context.PlaylistVideos.Add(new PlaylistVideo()
                {
                    Playlistguid = usersMainPlaylist.Guid,
                    Videoguid = videoGuid,
                    Added = DateTime.UtcNow
                });

                context.SaveChanges();
            }

            Console.WriteLine($"Downloaded video {videoTitle} from {platform} to {videoGuid}");

            return Ok();
        }
    }
}