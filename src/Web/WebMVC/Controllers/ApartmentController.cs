using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class ApartmentController: BasicController<ApartmentController> ,IPageController
    {
        public ApartmentController(IPagesNames pagesNames) : base(pagesNames.GetProperty()) { }
        
        [Authorize]
        public IActionResult New()
        {
            return ActionResult();
        }
    }
}