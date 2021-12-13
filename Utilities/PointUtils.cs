using Microsoft.Xna.Framework;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		public static Vector2 ToVector2(this Point vector) => new Vector2(vector.X, vector.Y);
	}
}