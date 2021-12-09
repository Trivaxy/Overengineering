using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Overengineering.Scenes
{
	public abstract class Scene
	{
		public IList<IDrawable> Drawables = new List<IDrawable>();
		public IList<ITickable> Tickables = new List<ITickable>();

		public void Update(GameTime time)
		{
			foreach (ITickable tickable in Tickables)
				tickable.Update(time);
		}

		public virtual void OnActivate() { }

		public virtual void OnDeactivate() { }
	}
}
