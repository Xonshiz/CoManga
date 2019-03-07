using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace comic_dl.commonFunctionalities
{
    static class CommonFunctions
    {
        public static string FileNameCleaner(string fileName)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, ' ');
            }
            return fileName;
        }

        public static string ToUpperCase(string StringToFormat)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(StringToFormat);
        }

        public static string DictionaryToJson(Dictionary<string, string> d)
        {
            // Build up each line one-by-one and then trim the end
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in d)
            {
                builder.Append('"' + pair.Key + '"').Append(":").Append('"' + pair.Value + '"').Append(',');
            }
            string result = builder.ToString();
            // Remove the final delimiter
            result = result.TrimEnd(',');
            return "{" + result + "}";
        }

        public static string ListToListString(List<string> StringList)
        {
            string ListString = "";
            foreach (string singleValue in StringList)
            {
                ListString += '"' + singleValue + '"' + ',';
            }
            return "[" + ListString.TrimEnd(',') + "]";
        }

        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }
    }
}
