using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			int width = original.GetLength(0);
			int height = original.GetLength(1);
			int t = (int)(whitePixelsFraction * width * height);
			var filter = new double[width, height];
			var list = new List<double>();
			foreach (var e in original)
					if (!list.Contains(e)) list.Add(e);
			if (t >= list.Count)
				for (int x = 0; x < width; x++)
					for (int y = 0; y < height; y++)
						filter[x, y] = 1.0;
			else
            {
				list.Sort();
				for (int i = 0; list.Count > t;)
					list.RemoveAt(i);
            }
			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
				{
					if (list.Contains(original[x, y])) filter[x, y] = 1.0;
					else filter[x, y] = 0.0;
				}
			return filter;
		}
	}
}