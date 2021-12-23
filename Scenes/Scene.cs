using Microsoft.Xna.Framework;
using Overengineering.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Overengineering.Scenes
{

    public abstract class Scene
    {
        public IList<IDrawable> Drawables = new List<IDrawable>();
        public IList<ITickable> Tickables = new List<ITickable>();

        public IList<IEntitySystem> Systems = new List<IEntitySystem>();

        public void UpdateTickables(GameTime time)
        {
            foreach (ITickable tickable in Tickables)
                tickable.Update(time);
        }

        public void UpdateSystems(GameTime time)
        {
            foreach (IEntitySystem system in Systems)
                system.Update(time);
        }

        public void AddEntityToSystems(Entity entity)
        {
            foreach (IEntitySystem e in Systems)
            {
                foreach (Type t in e.GetType().BaseType.GetGenericArguments())
                {
                    if (entity.GetType().IsSubclassOf(t))
                    {
                        e.AddEntity(entity);
                        break;
                    }
                }
            }
        }


        public void AddSystem<T>() where T : IEntitySystem, new() => Systems.Add(new T());

        public void Activate()
        {
            RegisterSystems();
            OnActivate();

            foreach (IEntitySystem system in Systems)
                system.Load();
        }

        public virtual void RegisterSystems() { }

        public virtual void Update(GameTime time) { }

        public virtual void OnActivate() { }

        public virtual void OnDeactivate() { }
    }
}

