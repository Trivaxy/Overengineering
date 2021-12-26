using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		// Center coordinates of a texture
		public static Vector2 Center(this Texture2D texture) => new Vector2(texture.Width / 2, texture.Height / 2);
	}
}
