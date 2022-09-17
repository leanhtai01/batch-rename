using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRename
{
    class StringProcessor
    {
        public static string Replace(string str, string replaced, string replaceWith)
        {
            return str.Replace(replaced, replaceWith);
        }

        public static string ToUpperFirstLetterOfWords(string str)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string FullnameNormalize(string str)
        {
            str = ToUpperFirstLetterOfWords(str);
            str = str.Trim();

            return Regex.Replace(str, @"\s+", " ");
        }
    }
}
