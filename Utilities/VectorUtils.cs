using Microsoft.Xna.Framework;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		public static Point ToPoint(this Vector2 vector) => new Point((int)vector.X, (int)vector.Y);

		public static Vector2 Project(this Vector3 vector, CameraTransform camera) => Renderer.Viewport.Project(vector, camera.ProjectionMatrix, camera.ViewMatrix, camera.WorldMatrix).NoZ();

		/// <summary>
		/// (X, Y, Z) -> (X, Y)
		/// </summary>
		public static Vector2 NoZ(this Vector3 vector) => new Vector2(vector.X, vector.Y);

		/// <summary>
		/// (X, Y, Z) -> (X, Z)
		/// </summary>
		public static Vector2 NoY(this Vector3 vector) => new Vector2(vector.X, vector.Z);

		/// <summary>
		/// (X, Y, Z) -> (Y, Z)
		/// </summary>
		public static Vector2 NoX(this Vector3 vector) => new Vector2(vector.Y, vector.Z);

		public static Vector2 Interpolate(Vector2 v1, Vector2 v2, float x) => new Vector2(v1.X + (v2.X - v1.X) * x, v1.Y + (v2.Y - v1.Y) * x);

		public static Vector3 Interpolate(Vector3 v1, Vector3 v2, float x) => new Vector3(v1.X + (v2.X - v1.X) * x, v1.Y + (v2.Y - v1.Y) * x, v1.Z + (v2.Z - v1.Z) * x);

		/// <summary>
		/// (X, Y) -> (X, Y, 0)
		/// </summary>
		public static Vector3 AddZ(this Vector2 vector) => new Vector3(vector.X, vector.Y, 0);

		/// <summary>
		/// (X, Y) -> (X, Y, Z)
		/// </summary>
		public static Vector3 WithZ(this Vector2 vector, float z) => new Vector3(vector.X, vector.Y, z);
	}
}