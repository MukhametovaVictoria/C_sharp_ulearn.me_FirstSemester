using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var median = new double[width, height];
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					var list = new List<double>();
					if (x - 1 >= 0)
                    {
						if (y - 1 >= 0) list.Add(original[x - 1, y - 1]);
						if (y+1 <= height - 1) list.Add(original[x - 1, y + 1]);
						list.Add(original[x - 1, y]);
					}
					if (x + 1 <= width - 1)
                    {
						if (y - 1 >= 0) list.Add(original[x + 1, y - 1]);
						if (y + 1 <= height - 1) list.Add(original[x + 1, y + 1]);
						list.Add(original[x + 1, y]);
					}
					if (y - 1 >= 0) list.Add(original[x, y - 1]);
					if (y + 1 <= height - 1) list.Add(original[x, y + 1]);
					list.Add(original[x, y]);
					list.Sort();
					int count = list.Count;
					if (count % 2 == 0 && count > 1) median[x, y] = (list[count / 2 - 1] + list[count / 2]) / 2;
					else median[x, y] = list[(count - 1) / 2];
				}
			return median;
		}
	}
}