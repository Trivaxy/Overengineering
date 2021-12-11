using Microsoft.Xna.Framework;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		public static Point ToPoint(this Vector2 vector) => new Point((int)vector.X, (int)vector.Y);

		public static Vector2 Project(this Vector3 vector, CameraTransform camera) => Renderer.Viewport.Project(vector, camera.ProjectionMatrix, camera.ViewMatrix, Matrix.CreateTranslation(vector)).NoZ();

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
	}
}