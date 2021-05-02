using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names
{
    internal static class CreativityTask
    {
        //12 июля по григорианскому календарю - славянский праздник "Петров день". 
        //Этот праздник посвящен апостолам Петру и Павлу
        //По статистике 12 июля самая большая рождаемость Петров и Павлов
        //Видимо при выборе имени родители опирались на праздничные дни
            public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names, string name1)
            {
                var birthDay = new string[30];
                for (int i = 1; i < 31; i++)
                    birthDay[i - 1] = (i + 1).ToString();

                var month = new string[12];
                for (int j = 0; j < 12; j++)
                    month[j] = (j + 1).ToString();

                var birthsCounts = new double[30, 12];
                foreach (var name in names)
                {
                    if ((name.BirthDate.Day - 2) >= 0 && name.Name == name1) birthsCounts[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
                    else continue;
                }
            Console.WriteLine("Число дней рождения в июле для имени '{0}'", name1);
            for (int k = 0; k < 30; k++)
            {
                Console.Write((2 + k) + " | ");
                Console.Write(birthsCounts[k,6]+ "\r\n");
            }
                return new HeatmapData(
                    string.Format("Пример карты интенсивностей для имени '{0}'", name1),
                    birthsCounts,
                    birthDay,
                    month);
            }
    }
}
