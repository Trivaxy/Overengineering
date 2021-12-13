using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
        public static void DrawText(string text, Color colour, Vector2 position, SpriteBatch sb, float layerDepth = 0f, float rotation = 0f, float scale = 0.5f, SpriteFont font = default)
        {
            font = font == default ? Assets<SpriteFont>.Get("Fonts/DefaultFont").GetValue() : font;

            Vector2 textSize = font.MeasureString(text);
            float textPositionLeft = position.X - textSize.X / 2;

            sb.DrawString(font, text, new Vector2(textPositionLeft, position.Y), colour, rotation, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }

        public static float DrawTextToLeft(string text, Color colour, Vector2 position, SpriteBatch sb, float layerDepth = 0f, float scale = 0.5f, SpriteFont font = default)
        {
            font = font == default ? Assets<SpriteFont>.Get("Fonts/DefaultFont").GetValue() : font;

            float textPositionLeft = position.X;
            sb.DrawString(font, text, new Vector2(textPositionLeft, position.Y), colour, 0f, Vector2.Zero, scale, SpriteEffects.None, layerDepth);

            return font.MeasureString(text).X;
        }
    }
}