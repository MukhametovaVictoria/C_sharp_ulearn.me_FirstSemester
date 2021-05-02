using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            float x1 = (float)Math.Cos(shoulder) * Manipulator.UpperArm;
            float y1 = (float)Math.Sin(shoulder) * Manipulator.UpperArm;
            var elbowAngel = shoulder + Math.PI + elbow;
            float x2 = x1 + (float)Math.Cos(elbowAngel) * Manipulator.Forearm;
            float y2 = y1 + (float)Math.Sin(elbowAngel) * Manipulator.Forearm;
            var wristAngel = shoulder + Math.PI + elbow + Math.PI + wrist;
            float x3 = x2 + (float)Math.Cos(wristAngel) * Manipulator.Palm;
            float y3 = y2 + (float)Math.Sin(wristAngel) * Manipulator.Palm;
            var elbowPos = new PointF(x1, y1);
            var wristPos = new PointF(x2, y2);
            var palmEndPos = new PointF(x3, y3);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm, 0)]
        [TestCase(3 * Math.PI / 2, Math.PI, Math.PI, 0, -(Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm))]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            //Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}