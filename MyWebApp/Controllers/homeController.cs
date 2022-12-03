using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class homeController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public ContentResult? Index()
        {
            try
            {
                return new ContentResult
                {
                    Content = "",
                    ContentType = "text/html"
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
