using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using System.Collections.Generic;
using Overengineering.Maths;
using Overengineering.UI;
using System;
using System.Linq;

namespace Overengineering
{
    public class PhysicsVector
    {
        public Vector3 point;
        public Vector3 oldPoint;
        public Vector3 vel;
        public bool isStatic;

        public static implicit operator PhysicsVector(Vector3 p) => new PhysicsVector() { point = p, oldPoint = p };
    }
    public class PhysicsPolygon
    {
        public PhysicsVector[] points;

        public Vector3 Center
        {
            get
            {
                if (vertexcount == 0) return Vector3.Zero;

                Vector3 total = Vector3.Zero;
                for (int i = 0; i < points.Length; i++) total += points[i].point;

                return total / vertexcount;
            }
        }

        public int vertexcount => points.Length;

        public static PhysicsPolygon Empty => new PhysicsPolygon(new PhysicsVector[] { });


        public static implicit operator Polygon(PhysicsPolygon p) => new Polygon(p.points.Select(p => p.point).ToArray());

        public static implicit operator PhysicsPolygon(Polygon p) => new PhysicsPolygon(p.points.Select(p => { return new PhysicsVector() { point = p }; }).ToArray());

        public PhysicsPolygon(PhysicsVector[] points) => this.points = points;
    }

    public class Constraint
    {
        public PhysicsVector p1;
        public PhysicsVector p2;

        public float Length;

        public Constraint(PhysicsVector p1, PhysicsVector p2)
        {
            this.p1 = p1;
            this.p2 = p2;

            Length = Vector3.Distance(p1.point, p2.point);
        }
    }

    public class VerletEntitySystem : EntitySystem<RigidBody>
    {
        IList<Constraint> constraints = new List<Constraint>();

        public void ConfigureConstraint(PhysicsPolygon poly)
        {
            //diferent hanging modes and such later.
            //for now all polys have bounding volume
            for (int i = 0; i < poly.points.Length; i++)
            {
                constraints.Add(new Constraint(poly.points[i], poly.points[(i + 1) % poly.points.Length]));
            }
        }

        public override void EntityWatch(in RigidBody entity)
        {
            foreach (PhysicsPolygon poly in entity.Polygons)
            {
                ConfigureConstraint(poly);
            }
        }

        public void ConstrainPoints()
        {
            foreach (RigidBody body in Entities)
            {
                foreach (PhysicsPolygon poly in body.Polygons)
                {
                    foreach (PhysicsVector point in poly.points)
                    {
                        if (!point.isStatic)
                        {
                            point.vel.X = (point.point.X - point.oldPoint.X) * body.airResistance;
                            point.vel.Y = (point.point.Y - point.oldPoint.Y) * body.airResistance;

                            if (point.point.Y > 0)
                            {
                                point.oldPoint.Y = point.vel.Y * body.resistution;
                                point.point.Y = 0;
                            }
                        }
                    }
                }
            }
        }

        public void UpdatePoints()
        {
            foreach (RigidBody body in Entities)
            {
                foreach (PhysicsPolygon poly in body.Polygons)
                {
                    foreach (PhysicsVector point in poly.points)
                    {
                        if (!point.isStatic)
                        {
                            point.vel.X = (point.point.X - point.oldPoint.X) * body.airResistance;
                            point.vel.Y = (point.point.Y - point.oldPoint.Y) * body.airResistance;
                            point.vel.Z = (point.point.Z - point.oldPoint.Z) * body.airResistance;

                            point.oldPoint.X = point.point.X;
                            point.oldPoint.Y = point.point.Y;
                            point.oldPoint.Z = point.point.Z;

                            point.point.X += point.vel.X;
                            point.point.Y += point.vel.Y;
                            point.point.Z += point.vel.Z;

                            point.point.Y += body.mass;
                        }
                    }
                }
            }
        }

        public void UpdateConstraints()
        {
            foreach (Constraint constraint in constraints)
            {
                PhysicsVector p1 = constraint.p1;
                PhysicsVector p2 = constraint.p2;

                float dx = p2.point.X - p1.point.X;
                float dy = p2.point.Y - p1.point.Y;
                float dz = p2.point.Z - p1.point.Z;

                float currentLength = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                float deltaLength = currentLength - constraint.Length;
                float perc = deltaLength / currentLength * 0.5f;

                float offsetX = perc * dx;
                float offsetY = perc * dy;
                float offsetZ = perc * dz;

                if (!p1.isStatic)
                {
                    p1.point.X += offsetX;
                    p1.point.Y += offsetY;
                    p1.point.Z += offsetZ;
                }

                if (!p2.isStatic)
                {
                    p2.point.X -= offsetX;
                    p2.point.Y -= offsetY;
                    p1.point.Z -= offsetZ;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePoints();

            for (int i = 0; i < 5; i++)
            {
                UpdateConstraints();
                ConstrainPoints();
            }
        }
    }
}
