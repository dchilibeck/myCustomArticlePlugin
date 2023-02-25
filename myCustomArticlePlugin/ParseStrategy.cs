using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace myCustomArticlePlugin
{
    class ParseStrategy
    {
        public static string ParseContent(string source)
        {
            if (String.IsNullOrEmpty(source))
                return "";
            else
            {
                char[] array = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }                   
                }

                return new string(array, 0, arrayIndex);
            }
        }

        public static int CountWords(string source)
        {
            int WordCount = 0;
            if (String.IsNullOrEmpty(source))
                return WordCount;
            else
            {
                string contentToCount = ParseContent(source);
                WordCount = contentToCount.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                return WordCount;
            }
        }
    }
}
