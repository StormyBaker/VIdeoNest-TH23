using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using VideoNestServer.JsonStructures;
using VideoNestServer.Model;
using VideoNestServer.Utilities;

namespace VideoNestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("me")]
        [Authorize]
        public ActionResult UserInformation_Me()
        {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid guid = new Guid(guidClaim.Value);

            using (VideonestContext context = new VideonestContext())
            {
                bool accountExists = context.Accounts.Any(x => x.Guid == guid);

                if (!accountExists)
                {
                    return Forbid();
                }

                object? account = (from accounts in context.Accounts
                                   where accounts.Guid == guid
                                   select new
                                   {
                                       Guid = accounts.Guid,
                                       Email = accounts.Email
                                   }).SingleOrDefault();

                Profile? profile = context.Profiles.SingleOrDefault(x => x.Accountguid == guid);

                return new JsonResultBuilder()
                    .set("account", account ?? new Dictionary<object, object>())
                    .set("profile", profile ?? new Profile() { Accountguid = guid })
                    .get();
            }
        }

        [HttpGet]
        [Route("profilepicture/{imageName}")]
        [Authorize]
        public IActionResult GetImage(string imageName)
        {
            string fileSavePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\ProfilePictures";

            try
            {
                var imagePath = Path.Combine(fileSavePath, imageName);

                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Image not found");
                }

                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/png");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("update")]
        [Authorize]
        public ActionResult UpdateUserInformation([FromForm] UserUpdate newData) {
            Claim? guidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "guid");

            if (guidClaim == null)
            {
                return Forbid();
            }

            Guid userGuid = new Guid(guidClaim.Value);

            string fileSavePath = Path.GetDirectoryName(Environment.ProcessPath) + @"\ProfilePictures";

            if (!Directory.Exists(fileSavePath))
            {
                Directory.CreateDirectory(fileSavePath);
            }

            string? filePath = null;
            string? savablePath = null;
            if (newData.ProfilePicture != null)
            {
                savablePath = Guid.NewGuid() + "_" + newData.ProfilePicture.FileName;
                filePath = Path.Combine(fileSavePath, savablePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newData.ProfilePicture.CopyTo(fileStream);
                }
            }

            using (VideonestContext context = new VideonestContext())
            {
                Profile? userProfile = context.Profiles.SingleOrDefault(x => x.Accountguid == userGuid);

                if (userProfile == null)
                {
                    userProfile = new Profile
                    {
                        Accountguid = userGuid,
                        Image = savablePath,
                        Username = newData.Username,
                        Biography = newData.Biography
                    };

                    context.Profiles.Add(userProfile);

                }
                else
                {
                    userProfile.Image = savablePath ?? userProfile.Image;
                    userProfile.Username = newData.Username ?? userProfile.Username;
                    userProfile.Biography = newData.Biography ?? userProfile.Biography;
                }

                context.SaveChanges();

                return new JsonResultBuilder().set("newUser", userProfile).get();
            }
        }
    }
}