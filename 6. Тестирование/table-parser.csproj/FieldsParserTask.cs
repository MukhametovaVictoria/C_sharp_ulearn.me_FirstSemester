using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("'x y'", new[] { "x y" })]
        [TestCase(@"a""b c d e""", new[] { "a", "b c d e" })]
        [TestCase(@"a""b c d e""f", new[] { "a", "b c d e", "f" })]
        [TestCase(@"  a""b c d e""", new[] { "a", "b c d e" })]
        [TestCase(@"""b c d e""f", new[] { "b c d e", "f" })]
        [TestCase(" 1 ", new[] { "1" })]
        [TestCase("", new string[0])]
        [TestCase("hello  world", new[] { "hello", "world" })]
        [TestCase("''", new[] { "" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase(@"""QF \""""", new[] { "QF \"" })]
        [TestCase(@"'QF \''", new[] { "QF \'" })]
        [TestCase(@"""\'""", new[] { "'" })]
        [TestCase("'\"'", new[] { "\"" })]
        [TestCase(@"""\'", new[] { "'" })]
        [TestCase(@"'a ", new[] { "a " })]
        [TestCase("\"\\\\\" b", new[] { "\\", "b" })]
        [TestCase("a \"bcd ef\" 'x y'", new[] { "a", "bcd ef", "x y" })]

        public static void RunTests(string input, string[] expectedOutput)
        {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        static int startIndex;
        public static List<Token> ParseLine(string line)
        {
            startIndex = 0;
            if (line.Contains('\"') || line.Contains('\''))
            {
                return ParseQuotedField(line);
            }
            else
            {
                return ParseFieldWithoutQuotes(line, startIndex, line.Length);
            }
        }

        private static Token ReadField(string line, int startIndex)
        {
            return new Token(line, startIndex, line.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }

        public static List<Token> ParseQuotedField(string line)
        {
            List<Token> tokenizedList = new List<Token>();
            List<Token> tokens = new List<Token>();
            int startQuote = 0;
            while (true)
            {
                if (startIndex == line.Length) break;
                for (int i = startIndex; i < line.Length; i++)
                {
                    if (line[i] == '\'' || line[i] == '\"')
                    {
                        startQuote = i;
                        break;
                    }
                }
                if (startIndex < startQuote)
                {
                    tokenizedList = ParseFieldWithoutQuotes(line, startIndex, startQuote);
                    tokens.AddRange(tokenizedList.GetRange(0, tokenizedList.Count));
                    tokenizedList = ParseFieldWithQuotes(line, startQuote);
                    tokens.AddRange(tokenizedList.GetRange(0, tokenizedList.Count));
                }
                else if (startIndex == startQuote)
                {
                    tokenizedList = ParseFieldWithQuotes(line, startQuote);
                    tokens.AddRange(tokenizedList.GetRange(0, tokenizedList.Count));
                }
                else
                {
                    tokenizedList = ParseFieldWithoutQuotes(line, startIndex, line.Length);
                    tokens.AddRange(tokenizedList.GetRange(0, tokenizedList.Count));
                    break;
                }
            }
            return tokens;
        }

        public static List<Token> ParseFieldWithoutQuotes(string line, int start, int finish)
        {
            List<Token> tokenizedList = new List<Token>();
            StringBuilder word = new StringBuilder();
            int finalLetter = 0;
            for (int k = start; k < finish; k++)
            {
                if (line[k] != ' ')
                {
                    word.Append(line[k]);
                    finalLetter = k + 1;
                }
                if (line[k] == ' ' || k + 1 == finish && line[k] != ' ')
                {
                    if (word.Length > 0)
                    {
                        string token = word.ToString();
                        tokenizedList.Add(ReadField(token, finalLetter - word.Length));
                        word.Clear();
                    }
                }
            }
            return tokenizedList;
        }

        public static List<Token> ParseFieldWithQuotes(string line, int start)
        {
            List<Token> tokenizedList = new List<Token>();
            var token = ReadQuotedField(line, start);
            tokenizedList.Add(token);
            startIndex = token.GetIndexNextToToken();
            return tokenizedList;
        }
    }
}