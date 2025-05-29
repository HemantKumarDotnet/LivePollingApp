using Microsoft.AspNetCore.Mvc;

namespace LivePollingApp.Controllers
{
    public class VoterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Vote()
        {
            return View();
        }
    }
}
