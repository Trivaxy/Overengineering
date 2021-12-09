using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Overengineering
{
    public interface IDrawable
    {
        void Draw(SpriteBatch sb);

        string Layer { get; set; }
    }
}
