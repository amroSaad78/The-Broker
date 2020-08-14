using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime;
using WebMVC.Services;


namespace WebMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenIdConnect", Roles ="Admin")]
    public abstract class BasicController<T> : Controller where T: IPageController
    {
        public readonly string _pageName;
        public BasicController(Dictionary<string,string> pagesNames)
        {
            if (!pagesNames.TryGetValue(typeof(T).Name.SubstrEnd(10), out _pageName))
            {
                _pageName = "Home";
            }
        }

        protected virtual IActionResult ActionResult()
        {
            SetPageName();
            return View(); 
        }
        protected virtual IActionResult ActionResult(object model)
        {
            SetPageName();
            return View(model); 
        }
        private void SetPageName() => ViewData["pageName"] = _pageName;

    }
}