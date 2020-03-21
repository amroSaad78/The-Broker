using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize]
    public abstract class BasicController<T> : Controller where T: IPageController
    {
        public readonly string _pageName;
        public BasicController(Dictionary<string,string> pagesNames)
        {
            string controllerName = typeof(T).Name;
            if (!pagesNames.TryGetValue(controllerName.Substring(0, controllerName .Length - 10), out _pageName))
            {
                _pageName = "Home";
            }
        }

        public virtual IActionResult ActionResult()
        {
            ViewData["pageName"] = _pageName;
            return View();
        }
    }
}