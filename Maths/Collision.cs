using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Overengineering.Graphics.Meshes;
using Overengineering.Resources;
using Overengineering.Scenes;
using Overengineering.UI;
using Overengineering.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Overengineering.Maths
{
    public enum Bound
    {
        None,
        Top,
        Bottom,
        Left,
        Right
    }

    public struct CollisionInfo
    {
        public Bound AABB;
        public Vector3 d;
        public static CollisionInfo Default => new CollisionInfo(Vector3.Zero, Bound.None);
        public CollisionInfo(Vector3 d, Bound AABB)
        {
            this.AABB = AABB;
            this.d = d;
        }
    }

    public struct Polygon
    {
        public Vector3[] points;

        public Vector3 Center
        {
            get
            {
                if (vertexcount == 0) return Vector3.Zero;

                Vector3 total = Vector3.Zero;
                for (int i = 0; i < points.Length; i++) total += points[i];

                return total / vertexcount;
            }
        }

        public int vertexcount => points.Length;

        public static Polygon Empty => new Polygon(new Vector3[] { });

        public static Polygon operator +(Polygon x, Polygon y)
        {
            if (x.vertexcount == y.vertexcount)
            {
                Vector3[] points = new Vector3[x.vertexcount];
                for (int i = 0; i < x.vertexcount; i++)
                {
                    points[i] = x.points[i] + y.points[i];
                }
                return new Polygon(points);
            }
            return Empty;
        }

        public Polygon(Vector3[] points) => this.points = points;

    }
    public static class Collision
    {
        public static bool IsInPolygon(this Point point, Polygon polygon)
        {
            bool result = false;
            var a = polygon.points.Last();

            foreach (var b in polygon.points)
            {
                if ((b.X == point.X) && (b.Y == point.Y))
                    return true;

                if ((b.Y == a.Y) && (point.Y == a.Y))
                {
                    if ((a.X <= point.X) && (point.X <= b.X))
                        return true;

                    if ((b.X <= point.X) && (point.X <= a.X))
                        return true;
                }

                if ((b.Y < point.Y) && (a.Y >= point.Y) || (a.Y < point.Y) && (b.Y >= point.Y))
                {
                    if (b.X + (point.Y - b.Y) / (a.Y - b.Y) * (a.X - b.X) <= point.X)
                        result = !result;
                }
                a = b;
            }
            return result;
        }

        public static CollisionInfo SAT(Polygon shape1, Polygon shape2)
        {
            Polygon[] shapes = new Polygon[] { shape1, shape2 };
            float overlap = float.PositiveInfinity;
            for (int a = 0; a < 2; a++)
            {
                for (int i = 0; i < shapes[a].vertexcount; i++)
                {
                    int b = (i + 1) % shapes[a].vertexcount;
                    Vector3 axis = shapes[a].points[b] - shapes[a].points[i];
                    Vector3 axisNormal = Vector3.Normalize(new Vector3(-axis.Y, axis.X, 0));
                    float aMax = float.NegativeInfinity;
                    float aMin = float.PositiveInfinity;
                    for (int j = 0; j < shape1.vertexcount; j++)
                    {
                        float projection = Vector3.Dot(axisNormal, shape1.points[j]);
                        aMax = Math.Max(projection, aMax);
                        aMin = Math.Min(projection, aMin);
                    }
                    float bMax = float.NegativeInfinity;
                    float bMin = float.PositiveInfinity;
                    for (int j = 0; j < shape2.vertexcount; j++)
                    {
                        float projection = Vector3.Dot(axisNormal, shape2.points[j]);
                        bMax = Math.Max(projection, bMax);
                        bMin = Math.Min(projection, bMin);
                    }
                    overlap = Math.Min(Math.Min(bMax, aMax) - Math.Max(bMin, aMin), overlap);
                    if (!(bMax >= aMin && aMax >= bMin))
                        return new CollisionInfo(Vector3.Zero, Bound.None);
                }
            }
            Vector3 disp = shape2.Center - shape1.Center;
            float s = disp.Length();

            return new CollisionInfo(new Vector3(overlap * disp.X / s, overlap * disp.Y / s, overlap * disp.Z / s), Bound.Top);
        }
    }
}