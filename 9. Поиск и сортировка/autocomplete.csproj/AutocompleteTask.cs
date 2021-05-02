using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            // тут стоит использовать написанный ранее класс LeftBorderTask
            var listOfString = new List<string> { };
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            for (int i = 0; i < count; i++)
                if (index + i < phrases.Count 
                    && phrases[index + i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) 
                    listOfString.Add(phrases[index + i]);
            var arrOfString = new string[listOfString.Count];
            if (listOfString.Count != 0) arrOfString = listOfString.ToArray();
            return arrOfString;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, leftIndex, phrases.Count);
            return rightIndex - leftIndex - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public static void TopByPrefix_IsEmpty_WhenNoPhrases(string prefix, int count, string[] expectedResult, List<string> phrases)
        {
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }
        [TestCase("c", 2, new string[0])]

        [Test]
        public void GetTopByPrefixTest()
        {
            var phrases = new List<string> { "hello", "hi", "hide", "item", "items" };
            var prefix = "hi";
            var count = 2;
            var expectedResult = new[] { "hi", "hide" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }
        

        [Test]
        public void GetTopByPrefixTestLessThanCount()
        {
            var phrases = new List<string> { "hello", "hi", "hide", "item", "items" };
            var prefix = "i";
            var count = 3;
            var expectedResult = new[] { "item", "items" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string> { "hello", "hi", "hide", "item", "items" };
            var prefix = "l";
            var expectedResult = 0;
            var result = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountByPrefixTest()
        {
            var phrases = new List<string> { "hello", "hi", "hide", "item", "items" };
            var prefix = "h";
            var expectedResult = 3;
            var result = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
