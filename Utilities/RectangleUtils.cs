using Microsoft.Xna.Framework;
using System;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		public static Rectangle BoundingBox(Span<Vector2> points)
		{
			int minX = int.MaxValue;
			int minY = int.MaxValue;
			int maxX = int.MinValue;
			int maxY = int.MinValue;

			for (int i = 0; i < points.Length; i++)
			{
				Vector2 v = points[i];

				if (v.X < minX)
					minX = (int)v.X;
				else if (v.X > maxX)
					maxX = (int)v.X;

				if (v.Y < minY)
					minY = (int)v.Y;
				else if (v.Y > maxY)
					maxY = (int)v.Y;
			}

			return new Rectangle(minX, minY, maxX - minX, maxY - minY);
		}

		public static Rectangle BoundingBox(Vector2 v1, Vector2 v2, Vector2 v3)
			=> BoundingBox(stackalloc[] { v1, v2, v3 });

		public static Rectangle BoundingBox(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4)
			=> BoundingBox(stackalloc[] { v1, v2, v3, v4 });
	}
}
