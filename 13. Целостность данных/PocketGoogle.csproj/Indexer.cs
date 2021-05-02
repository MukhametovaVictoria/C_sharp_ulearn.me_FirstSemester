using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private char[] charArr = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        private Dictionary<string, Dictionary<int, List<int>>> dictionaryOfWords;

        public void Add(int id, string documentText)
        {
            var document = documentText.Split(charArr, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in document)
            {
                if (!dictionaryOfWords.ContainsKey(word))
                {
                    dictionaryOfWords.Add(word, new Dictionary<int, List<int>>());
                    dictionaryOfWords[word].Add(id, GetWordPositions(word, documentText));
                }
                else if (!dictionaryOfWords[word].ContainsKey(id))
                {
                    dictionaryOfWords[word].Add(id, GetWordPositions(word, documentText));
                }
            }
        }


        private List<int> GetWordPositions(string word, string text)
        {
            var result = new List<int>();
            for (int value = 0; value < text.Length; value++)
            {
                int index = text.IndexOf(word, value);
                if (index > -1)
                {
                    if (index + word.Length == text.Length
                        || index + word.Length < text.Length && !Char.IsLetter(text[index + word.Length])) result.Add(index);
                    value = index + word.Length;
                }
                else break;
            }
            
            return result;
        }

        public List<int> GetIds(string word)
        {
            if (dictionaryOfWords.ContainsKey(word)) return new List<int>(dictionaryOfWords[word].Keys);
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (dictionaryOfWords.ContainsKey(word))
                if (dictionaryOfWords[word].ContainsKey(id))
                    return dictionaryOfWords[word][id];
            return new List<int>();
        }

        public void Remove(int id)
        {
            foreach (var word in dictionaryOfWords.Keys)
            {
                if (dictionaryOfWords[word].ContainsKey(id))
                    dictionaryOfWords[word].Remove(id);
            }
        }

        public Indexer()
        {
            dictionaryOfWords = new Dictionary<string, Dictionary<int, List<int>>>();
        }
    }
}
