using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        //.Replace(',', ' ')
        //        .Replace('^', ' ')
        //        .Replace('#', ' ')
        //        .Replace('$', ' ')
        //        .Replace('-', ' ')
        //        .Replace('+', ' ')
        //        .Replace('1', ' ')
        //        .Replace('=', ' ')
        //        .Replace('“', ' ')
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            char[] sep = { '.', ';', ':', '!', '?', '(', ')' };
            var sentencesArr = text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sentencesArr.Length; i++)
            {
                sentencesArr[i] = Regex.Replace(sentencesArr[i], @"([^A-Za-z'])", " ");
                List<string> wordsList = new List<string>();
                var words = sentencesArr[i].Split();
                foreach (var word in words)
                {
                    if (word != "")
                    {
                        wordsList.Add(word.ToLower());
                    }
                }
                if (wordsList.Capacity > 0) sentencesList.Add(wordsList);
            }

            return sentencesList;
        }
    }
}