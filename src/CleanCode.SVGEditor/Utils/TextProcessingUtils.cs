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
    }
}
