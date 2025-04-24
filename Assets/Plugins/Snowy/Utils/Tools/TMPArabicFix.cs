namespace Snowy.Utils.Tools
{
    public static class TMPArabicFix
    {
        public static string Fix(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            char[] charArray = text.ToCharArray();
            System.Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}