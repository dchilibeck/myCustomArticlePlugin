using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chilibeck.myCustomArticlePlugin
{
    class ParseStrategy
    {
        public static string ParseContent(string source)
        {
            if (String.IsNullOrEmpty(source))
                return "";
            else
            {
                char[] itemArray = new char[source.Length];
                int arrayIndex = 0;
                bool inside = false;

                for (int i = 0; i < source.Length; i++)
                {
                    char item = source[i];
                    if (item == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (item == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        itemArray[arrayIndex] = item;
                        arrayIndex++;
                    }                   
                }

                return new string(itemArray, 0, arrayIndex);
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
            }

            return WordCount;
        }
    }
}
