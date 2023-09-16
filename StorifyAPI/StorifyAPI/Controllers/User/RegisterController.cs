using Microsoft.AspNetCore.Mvc;

namespace StorifyAPI.Controllers.User
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
