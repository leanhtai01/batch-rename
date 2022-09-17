using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BatchRename
{
    class NameProcessor
    {
        public static void PreProcess(string fullname, ref string filename, ref string extension)
        {
            string[] tmp = fullname.Split('.');

            filename = tmp[0];
            extension = "";
            if (tmp.Length > 1)
            {
                for (var i = 1; i < tmp.Length - 1; ++i)
                {
                    filename += '.' + tmp[i];
                }

                extension = '.' + tmp[tmp.Length - 1];
            }
        }

        public static string Replace(string fullname, string replaced, string replaceWith)
        {
            //string filename = "";
            //string extension = "";

            //PreProcess(fullname, ref filename, ref extension);
            //filename = StringProcessor.Replace(filename, replaced, replaceWith);

            //return filename + extension;
            return StringProcessor.Replace(fullname, replaced, replaceWith);
        }

        public static string ToUpper(string fullname)
        {
            //string filename = "";
            //string extension = "";

            //PreProcess(fullname, ref filename, ref extension);
            //filename = filename.ToUpper();

            //return filename + extension;
            return fullname.ToUpper();
        }

        public static string ToLower(string fullname)
        {
            //string filename = "";
            //string extension = "";

            //PreProcess(fullname, ref filename, ref extension);
            //filename = filename.ToLower();

            //return filename + extension;
            return fullname.ToLower();
        }

        public static string ToUpperFirstLetterOfWords(string fullname)
        {
            //string filename = "";
            //string extension = "";

            //PreProcess(fullname, ref filename, ref extension);
            //filename = StringProcessor.ToUpperFirstLetterOfWords(filename);

            //return filename + extension;
            return StringProcessor.ToUpperFirstLetterOfWords(fullname);
        }

        public static string FullnameNormalize(string fullname)
        {
            string filename = "";
            string extension = "";

            PreProcess(fullname, ref filename, ref extension);
            filename = StringProcessor.FullnameNormalize(filename);

            return filename + extension;
            //return StringProcessor.FullnameNormalize(fullname);
        }

        public static string UniqueName(string fullname)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(fullname));
                Guid result = new Guid(hash);
                fullname = result.ToString();
            }

            return fullname;
        }

        public static string Move(string fullname, int moveFrom, int moveCount, int moveTo)
        {
            //string filename = "";
            //string extension = "";

            //PreProcess(fullname, ref filename, ref extension);
            //if (moveFrom >= filename.Length || (moveFrom + moveCount) >= filename.Length || moveTo >= filename.Length)
            //{
            //    return fullname;
            //}
            //else
            //{
            //    string junk = "";
            //    for (var i = 0; i < moveCount; ++i) junk += '$';

            //    var subStr = filename.Substring(moveFrom, moveCount);
            //    var tmp = filename.Substring(0, moveFrom) + junk + filename.Substring(moveFrom + moveCount);

            //    filename = tmp.Substring(0, moveTo + 1) + subStr + tmp.Substring(moveTo + 1);
            //    filename = filename.Replace("$", "");
            //}

            //return filename + extension;
            if (!(moveFrom >= fullname.Length || (moveFrom + moveCount) >= fullname.Length || moveTo >= fullname.Length))
            {
                string junk = "";
                for (var i = 0; i < moveCount; ++i) junk += '$';

                var subStr = fullname.Substring(moveFrom, moveCount);
                var tmp = fullname.Substring(0, moveFrom) + junk + fullname.Substring(moveFrom + moveCount);

                fullname = tmp.Substring(0, moveTo + 1) + subStr + tmp.Substring(moveTo + 1);
                fullname = fullname.Replace("$", "");
            }

            return fullname;
        }
    }
}
