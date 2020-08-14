using Microsoft.AspNetCore.Mvc;

namespace WebClientAgg.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
