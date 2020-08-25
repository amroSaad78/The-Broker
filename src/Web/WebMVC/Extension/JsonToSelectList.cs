using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebMVC.Extension
{
    public static class JsonToSelectList
    {
        public static IEnumerable<SelectListItem> GetSelectListAsync(this string response, string listKey, string text)
        {
            var list = new List<SelectListItem>();
            JObject obj = JObject.Parse(response);
            var json = JArray.Parse(obj[listKey].ToString());
            foreach (var item in json?.Children<JObject>())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>(text)
                });
            }
            return list;
        }
    }
}
