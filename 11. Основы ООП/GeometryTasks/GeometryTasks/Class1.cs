using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vec2)
        {
            return Geometry.Add(this, vec2);
        }

        public bool Belongs(Segment seg)
        {
            return Geometry.IsVectorInSegment(this, seg);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vec)
        {
            return Geometry.IsVectorInSegment(vec, this);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vec1, Vector vec2)
        {
            return new Vector() { X = vec1.X + vec2.X, Y = vec1.Y + vec2.Y };
        }

        public static double GetLength(Segment seg)
        {
            return Math.Sqrt((seg.Begin.X - seg.End.X) * (seg.Begin.X - seg.End.X)
                             + (seg.Begin.Y - seg.End.Y) * (seg.Begin.Y - seg.End.Y));
        }

        public static bool IsVectorInSegment(Vector vec, Segment seg)
        {
            var seg1 = GetLength(new Segment()
            { Begin = new Vector() { X = seg.Begin.X, Y = seg.Begin.Y }, End = vec });
            var seg2 = GetLength(new Segment()
            { Begin = vec, End = new Vector() { X = seg.End.X, Y = seg.End.Y } });
            var full = GetLength(seg);
            return seg1 + seg2 == full ? true : false;
        }
    }
}
