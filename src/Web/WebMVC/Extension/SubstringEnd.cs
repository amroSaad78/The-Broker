namespace System.Runtime
{
    public static class SubstringEnd
    {
        public static string SubstrEnd(this string input, int length, int startIndex = 0)
        {
            if (String.IsNullOrEmpty(input)) throw new ArgumentNullException(input, "Input value cannot be null or empty");
            if (startIndex < 0) throw new ArgumentException(startIndex.ToString(), "Value must be greater than zero");
            length = Math.Abs(length);
            return input.Substring(startIndex, input.Length - length);
        }
    }
}
