using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var birthDay = new string[30];
            for (int i = 1; i < 31; i++)
                birthDay[i-1] = (i + 1).ToString();

            var month = new string[12];
            for (int j = 0; j < 12; j++)
                month[j] = (j + 1).ToString();

            var birthsCounts = new double[30, 12];
            foreach (var name in names)
            {
                if ((name.BirthDate.Day - 2) >= 0) birthsCounts[name.BirthDate.Day - 2, name.BirthDate.Month-1]++;
                else continue;
            }
            return new HeatmapData(
                "Пример карты интенсивностей",
                birthsCounts,
                birthDay, 
                month);
        }
    }
}