using System;

namespace AutomationFramework.Helpers
{
    public class Utils
    {
        public static string uniqueString = Guid.NewGuid().ToString().Substring(0, 5);

        public static string ReplaceText(string text)
        {
            if (text.Contains("<UNIQUE>"))
            {
                text = text.Replace("<UNIQUE>", uniqueString);
            }
            return text;
        }
    }
}
