using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using System.Collections.Generic;

namespace Overengineering
{
	//for grouping
	public interface IEntitySystem : ITickable 
	{
		public void AddEntity(Entity entity);
	}
	public abstract class EntitySystem<T> : IEntitySystem where T : Entity
	{
		public List<T> Entities = new List<T>();

		public void AddEntity(Entity entity)
		{
			if(entity is T e) Entities.Add(e);
		}
		
		public abstract void Update(GameTime gameTime);
	}
}
