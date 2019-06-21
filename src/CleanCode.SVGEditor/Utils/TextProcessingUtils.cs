using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.SVGEditor.Utils
{
    internal static class TextProcessingUtils
    {
        /// <summary>
        /// Splits text by spaces but doesn't split quoted text.
        /// </summary>
        /// <param name="source">The source text.</param>
        /// <returns>IList of splitted tokens.</returns>
        public static IList<string> SplitTokens(string source)
        {
            string currentToken = string.Empty;
            bool inQuotes = false;

            IList<string> tokens = new List<string>();

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == '\"')
                {
                    if (inQuotes)
                    {
                        tokens.Add(currentToken);
                        currentToken = string.Empty;
                    }

                    inQuotes = !inQuotes;
                    continue;
                }

                if ((source[i] != ' ') || inQuotes) //If we are in quotes we push everything except the closing quote.
                {
                    if (source[i] != '\"')
                    {
                        currentToken += source[i];
                    }

                    if (i == source.Length - 1)
                    {
                        tokens.Add(currentToken);
                        currentToken = string.Empty;
                    }
                }
                else if ((source[i] == ' ') && !string.IsNullOrEmpty(currentToken))
                {
                    tokens.Add(currentToken);
                    currentToken = string.Empty;
                }
            }

            return tokens;
        }

        /// <summary>
        /// Splits attributes with equal sign to key and value.
        /// </summary>
        /// <param name="arguments">The source text splitted by spaces.</param>
        /// <returns>IDictionary with splited keys and values.</returns>
        public static IDictionary<string, string> SplitAttributes(IList<string> arguments)
        {
            IDictionary<string, string> attributes = new Dictionary<string, string>();
            foreach (var arg in arguments)
            {
                int equalsPosition = arg.IndexOf('=');
                if (equalsPosition != -1)
                {
                    string key = arg.Substring(0, equalsPosition);
                    string value = arg.Substring(equalsPosition + 1, arg.Length - equalsPosition - 1);
                    attributes.Add(key, value);
                }
            }

            return attributes;
        }
    }
}
