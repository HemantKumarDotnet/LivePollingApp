using Microsoft.AspNetCore.Mvc;

namespace LivePollingApp.Controllers
{
    public class Polling : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
