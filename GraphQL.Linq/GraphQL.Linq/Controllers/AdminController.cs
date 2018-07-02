using Microsoft.AspNetCore.Mvc;

namespace WPS.Infrastructure.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}