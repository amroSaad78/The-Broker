using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMVC.Extension
{
    public static class Contains
    {
        public static string ContaineStr(this IEnumerable<string> enumration, string pageName)
        {
            if (String.IsNullOrEmpty(pageName)) return "";
            return enumration.Contains(pageName) ? "active" : "";
        }
    }
}
