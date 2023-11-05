using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using VideoNestServer.JsonStructures;
using VideoNestServer.Model;
using VideoNestServer.Utilities;

namespace VideoNestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly TokenManager _tokenManager;

        public AccountController(TokenManager manager) {
            _tokenManager = manager;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(AccountCredentials credentials) { 
            using (VideonestContext context = new VideonestContext())
            {
                bool accountExists = context.Accounts.Any(x => x.Email == credentials.email);

                if (!accountExists)
                {
                    return Unauthorized();
                }

                Account account = context.Accounts.Single(x => x.Email == credentials.email);

                bool correctInformation = BCrypt.Net.BCrypt.Verify(credentials.password, account.Password);

                if (!correctInformation)
                {
                    return Unauthorized();
                }

                Response.Cookies.Append("token", _tokenManager.GenerateToken(account), new CookieOptions
                {
                    HttpOnly = true
                });

                return Ok();
            }
        }

        [HttpPost]
        [Route("register")]
        public JsonResult Register([FromBody] AccountCredentials credentials)
        {
            using (VideonestContext context = new VideonestContext())
            {
                bool accountAlreadyRegistered = context.Accounts.Any(x => x.Email == credentials.email);

                if (accountAlreadyRegistered)
                {
                    Response.StatusCode = 409;
                    return new JsonResultBuilder().set("error", "Email already registered.").get();
                }

                Guid newUserGuid = Guid.NewGuid();

                context.Accounts.Add(new Account()
                {
                    Guid = newUserGuid,
                    Email = credentials.email,
                    Password = BCrypt.Net.BCrypt.HashPassword(credentials.password)
                });

                Guid newPlaylistGuid = Guid.NewGuid();

                context.Playlists.Add(new Playlist()
                {
                    Guid = newPlaylistGuid,
                    Name = "My Nest",
                    Description = "A nest containing every video saved.",
                    Type = (int)Enum.PlaylistTypeDef.Main
                });

                context.PlaylistAccesses.Add(new PlaylistAccess()
                {
                    Playlistguid = newPlaylistGuid,
                    Accountguid = newUserGuid,
                    Accesslevel = (int)Enum.PlaylistAccessLevelDef.Owner
                });

                context.SaveChanges();

                Response.StatusCode = 200;
                return new JsonResultBuilder().set("success", "Account successfully registered.").get();
            }
        }
    }
}