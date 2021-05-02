using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            var shift = sx.GetLength(0) / 2;
            for (int x = shift; x < width - shift; x++)
                for (int y = shift; y < height - shift; y++)
                {
                    var gx = MultiplyPixels(g, sx, x, y, shift);
                    var gy = MultiplyPixels(g, sy, x, y, shift);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        public static double[,] TransposeMatrix(double[,] sx)
        {
            var heigth = sx.GetLength(0);
            var width = sx.GetLength(1);
            var sy = new double[width, heigth];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < heigth; y++)
                    sy[x, y] = sx[y, x];
            return sy;
        }

        public static double MultiplyPixels(double[,] pixels, double[,] sMatrix, int x, int y, int shift)
        {
            double result = 0;
            int sideLength = sMatrix.GetLength(0);
            for (int i = 0; i < sideLength; i++)
                for (int j = 0; j < sideLength; j++)
                    result += sMatrix[i, j] * pixels[x - shift + i, y - shift + j];
            return result;
        }
    }
}