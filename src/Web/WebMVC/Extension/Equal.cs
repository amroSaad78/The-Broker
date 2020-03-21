using System;

namespace WebMVC.Extension
{
    public static class Equal
    {
        public static string EqualStr(this string left, string right)
        {
            if (String.IsNullOrEmpty(left) || String.IsNullOrEmpty(right)) return "";
            return left == right ? "active" : "";
        }
    }
}
