using Microsoft.AspNetCore.Mvc;

namespace LivePollingApp.Controllers
{
    public class CandidateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Participate()
        {
            return View();
        }
    }
}
