using Microsoft.Xna.Framework.Graphics;

namespace Overengineering
{
    public interface IDrawable
    {
        void Draw(SpriteBatch sb);

        string Layer { get; }
    }
}