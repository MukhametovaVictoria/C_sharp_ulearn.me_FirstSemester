using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name1)
        {
            var birthDay = new string[31];
            for (int i = 0; i < 31; i++)
                birthDay[i] = (i + 1).ToString();

            var birthsCounts = new double[31];
            foreach (var name in names)
            {
                if (name1 == name.Name)
                {
                    if ((name.BirthDate.Day - 1) == 0) birthsCounts[name.BirthDate.Day - 1] = 0;
                    else birthsCounts[name.BirthDate.Day - 1]++;
                }
            }

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name1),
                birthDay,
                birthsCounts);
        }
    }
}