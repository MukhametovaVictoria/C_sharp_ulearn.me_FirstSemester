using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (int startIndex = 0; startIndex < sentence.Count - 1; startIndex++)
                {
                    for (int numberOfWords = 1; numberOfWords < 3
                        && startIndex + numberOfWords < sentence.Count; numberOfWords++)
                    {
                        string key = string.Join(" ", sentence.GetRange(startIndex, numberOfWords).ToArray());
                        string gramma = sentence[numberOfWords + startIndex];
                        if (!frequencyDictionary.ContainsKey(key))
                            frequencyDictionary.Add(key, new Dictionary<string, int>() { { gramma, 1 } });
                        else
                            if (!frequencyDictionary[key].ContainsKey(gramma))
                                frequencyDictionary[key][gramma] = 1;
                            else frequencyDictionary[key][gramma]++;
                    }
                }
            }

            //не подходит
            //foreach (var e in couple)
            //{
            //    result[e.Key] = e.Value
            //        .First(x => x.Value == e.Value.Values.Max())
            //        .Key;
            //}
            result = FilterDictionary(frequencyDictionary);
            return result;
        }

        public static Dictionary<string, string> FilterDictionary(Dictionary<string,
Dictionary<string, int>> frequencyDictionary)
        {
            var filterDictionary = new Dictionary<string, string>();
            foreach (var key in frequencyDictionary.Keys)
            {
                string maxString = "";
                int maxValue = -1;
                foreach (var key2 in frequencyDictionary[key])
                {
                    if (key2.Value > maxValue)
                    {
                        maxString = key2.Key;
                        maxValue = key2.Value;
                    }
                    else if (key2.Value == maxValue)
                    {
                        if (string.CompareOrdinal(key2.Key, 0, maxString, 0, 15) < 0)
                            maxString = key2.Key;
                    }
                }
                filterDictionary[key] = maxString;
            }
            return filterDictionary;
        }
    }
}
//using System.Collections.Generic;
//using System.Linq;

//namespace TextAnalysis
//{
//    static class FrequencyAnalysisTask
//    {
//        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
//        {
//            var result = new Dictionary<string, string>();
//            var frequency = new Dictionary<string, int>();
//            var keysList = new List<string>();
//            for (int i = 0; i < text.Count; i++)
//            {
//                for (int startIndex = 0; startIndex < text[i].Count - 1; startIndex++)
//                {
//                    for (int numberOfWords = 1; numberOfWords < 3
//                         && startIndex + numberOfWords < text[i].Count; numberOfWords++)
//                    {
//                        string[] output = text[i].GetRange(startIndex,
//                                                           numberOfWords + 1).ToArray();
//                        string str = string.Join(" ", output);
//                        if (!frequency.ContainsKey(str)) frequency[str] = 0;
//                        else frequency[str]++;
//                        string[] output2 = text[i].GetRange(startIndex,
//                                                            numberOfWords).ToArray();
//                        string str2 = string.Join(" ", output2);
//                        if (!keysList.Contains(str2)) keysList.Add(str2);
//                    }
//                }
//            }
//            foreach (var e in keysList)
//            {
//                if (!result.ContainsKey(e))
//                    result[e] = frequency
//                        .Where(c => c.Key.StartsWith(e))
//                        .Where(y => y.Key.Length - 2 >= e.Length)
//                        .OrderBy(s => s.Key)
//                        .First(x => x.Value == frequency.Values.Max())
//                        .Key.Substring(e.Length + 1);
//            }
//            return result;
//        }
//    }
//}