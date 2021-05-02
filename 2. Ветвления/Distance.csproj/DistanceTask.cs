using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
        static double angle = 0;
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
            double abByX = ax - bx; //длина отрезка по оси Х
            double abByY = ay - by; //длина отрезка по оси У
            double modAB = Math.Sqrt((abByX * abByX + abByY * abByY)); //диагональ прямоугольного треугольника со сторонами abByX abByY
            //находим расстояния от точки Х до А и от Х до В,
            //складываем их и округляем до 5 знаков после запятой,
            //затем сравниваем с диагональю прямоугольного треугольника со сторонами abByX abByY
            //таким образом узнаем лежит ли точка на самом отрезке АВ
            if (Math.Round((DistanceToPoint(x, y, ax, ay) + DistanceToPoint(x, y, bx, by)),5) == Math.Round((modAB),5))
                { return 0; }
            //если расстояние от точки Х до А меньше, чем от Х до В
            else if (DistanceToPoint(x, y, ax, ay) < (DistanceToPoint(x, y, bx, by)))
            {
                //находим угол между АХ и АВ
                angle = Angle(x, y, ax, ay, bx, by);
                if (angle < 1.57) //1,57 - это пи/2, т.е 90 градусов
                {
                    return Math.Sin(angle) * DistanceToPoint(x, y, ax, ay);
                }
                else
                {
                    return DistanceToPoint(x, y, ax, ay);
                }
            }
            //если расстояние от точки Х до В меньше, чем от Х до А
            else
            {
                //находим угол между ВХ и АВ
                angle = Angle(x, y, bx, by, ax, ay);
                if (angle < 1.57)
                {
                    return Math.Sin(angle) * DistanceToPoint(x, y, bx, by);
                }
                else
                {
                    return DistanceToPoint(x, y, bx, by);
                }
            }
        }

        //нахождение расстояний от Х до А или от Х до В через прямоугольный треугольник, который они образуют
        public static double DistanceToPoint(double x, double y, double aX, double aY)
        {
            return Math.Sqrt((aX - x) * (aX - x) + (aY - y) * (aY - y));
        }

        public static double Angle(double x, double y, double ax, double ay, double bx, double by)
        {
            double abX = ax - bx;
            double abY = ay - by;
            double xaX = ax - x;
            double xaY = ay - y;
            double modAB = Math.Sqrt((abX * abX + abY * abY)); //диагональ АВ прямоугольного треугольника
            double modXA = Math.Sqrt((xaX * xaX + xaY * xaY)); //диагональ АХ прямоугольного треугольника
            return Math.Acos(((abX * xaX) + (abY * xaY)) / (modAB * modXA)); //какая-то чудесная формула нахождения угла между АХ и АВ
            //аналогично применяется для находения угла между АВ и ВХ
		}
	}
}