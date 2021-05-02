using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphic;

        public static void Initialize ( Graphics newGraphic )
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; 
            y = y0;
        }

        public static void MakeIt(Pen pen, double length, double angel)
        {
        //Делает шаг длиной length в направлении angel и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angel));
            var y1 = (float)(y + length * Math.Sin(angel));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angel)
        {
            x = (float)(x + length * Math.Cos(angel)); 
            y = (float)(y + length * Math.Sin(angel));
        }
    }

    public class ImpossibleSquare
    {
        public static void DrawTheFirstSide(int width, int height)
        {
            var sz = Math.Min(width, height);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, 0);
            Painter.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 4);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2);

            Painter.Change(sz * 0.04f, -Math.PI);
            Painter.Change(sz * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }

        public static void DrawTheSecondSide(int width, int height)
        {
            var sz = Math.Min(width, height);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2);
            Painter.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2 + Math.PI);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, -Math.PI / 2 + Math.PI / 2);

            Painter.Change(sz * 0.04f, -Math.PI / 2 - Math.PI);
            Painter.Change(sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        public static void DrawTheThirdSide(int width, int height)
        {
            var sz = Math.Min(width, height);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Painter.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI + Math.PI);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI + Math.PI / 2);

            Painter.Change(sz * 0.04f, Math.PI - Math.PI);
            Painter.Change(sz * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        public static void DrawTheFourthSide(int width, int height)
        {
            var sz = Math.Min(width, height);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2);
            Painter.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2 + Math.PI);
            Painter.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2 + Math.PI / 2);

            Painter.Change(sz * 0.04f, Math.PI / 2 - Math.PI);
            Painter.Change(sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        public static void Draw(int width, int height, double rotationAngel, Graphics graphic)
        {
            // rotationAngel пока не используется, но будет использоваться в будущем
            Painter.Initialize(graphic);

            var sz = Math.Min(width, height);

            var diagonalLength = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Painter.SetPosition(x0, y0);
            //Рисуем 1-ую сторону
            DrawTheFirstSide(width, height);
            //Рисуем 2-ую сторону
            DrawTheSecondSide(width, height);
            //Рисуем 3-ю сторону
            DrawTheThirdSide(width, height);
            //Рисуем 4-ую сторону
            DrawTheFourthSide(width, height);
        }
    }
}