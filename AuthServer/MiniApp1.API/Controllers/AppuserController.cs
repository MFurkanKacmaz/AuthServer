using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class AppuserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUserList()
        {
            var userName = HttpContext.User.Identity!.Name;
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return Ok("Kullanıcı listesi başarıyla iletildi");
        }
    }
}
