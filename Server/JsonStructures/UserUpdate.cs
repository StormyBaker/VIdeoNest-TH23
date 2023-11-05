using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VideoNestServer.JsonStructures
{
    public class UserUpdate
    {
        [FromForm(Name = "Username")]
        public string? Username { get; set; }
        [FromForm(Name = "Biography")]
        public string? Biography { get; set; }
        [FromForm(Name = "ProfilePicture")]
        public IFormFile? ProfilePicture { get; set; }
    }
}
