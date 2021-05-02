using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords,string phraseBeginning,
            int wordsCount)
        {
            string str = "";
            if (wordsCount > 0)
                for (int i = 0; i < wordsCount; i++)
                {
                    var words = phraseBeginning.Split(' ');
                    int count = words.Length;
                    if (count > 1)
                    {
                        str = words[count - 2] + " " + words[count - 1];
                        if (nextWords.ContainsKey(str))
                            phraseBeginning += " " + nextWords[str];
                        else if (nextWords.ContainsKey(words[count - 1]))
                            phraseBeginning += " " + nextWords[words[count - 1]];
                        else break;
                    }
                    else if (count == 1)
                    {
                        if (nextWords.ContainsKey(phraseBeginning))
                            phraseBeginning += " " + nextWords[phraseBeginning];
                    }
                    else break;
                }
            return phraseBeginning;
        }
    }
}