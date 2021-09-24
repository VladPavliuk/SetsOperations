using System.Collections.Generic;

namespace SetsOperations
{
    public static class Lexer
    {
        public static IEnumerable<string> Tokenize(string source)
        {
            var tokens = new List<string>();
            var index = 0;
            
            while (index < source.Length)
            {
                tokens.Add(GetNextToken(source, ref index));
            }

            return tokens;
        }

        private static string GetNextToken(string source, ref int index)
        {
            var alphabet = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var token = string.Empty;

            for (; index < source.Length; index++)
            {
                var currentSymbol = source[index].ToString();

                if (alphabet.Contains(currentSymbol))
                {
                    token += currentSymbol;
                }
                else if (!string.IsNullOrEmpty(token))
                {
                    index--;
                    break;
                }
                else if (!string.IsNullOrWhiteSpace(currentSymbol))
                {
                    token = currentSymbol;
                    break;
                }
            }

            index++;
            return token;
        }
    }
}
