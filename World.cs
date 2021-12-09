using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Assets;
using Overengineering.Graphics;
using System.Collections.Generic;

namespace Overengineering
{
    public class World
    {
        public List<IDrawable> Drawables = new List<IDrawable>();
        public List<ITickable> Tickables = new List<ITickable>();

        public void Update(GameTime gameTime)
        {
            foreach (ITickable tickable in Tickables)
                tickable.Update(gameTime);
        }

        public void AddEntity(Entity entity)
        {
            if (entity is IDrawable draw) Drawables.Add(draw);
            if (entity is ITickable tick) Tickables.Add(tick);
        }
    }
}
