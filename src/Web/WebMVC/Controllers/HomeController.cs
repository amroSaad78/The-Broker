using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class HomeController : BasicController<HomeController>, IPageController
    {
        public HomeController(IPagesNames pagesNames) :
            base(pagesNames.GetProperty())
        {

        }

        [AllowAnonymous]
        public IActionResult Index() => ActionResult();
    }
}
