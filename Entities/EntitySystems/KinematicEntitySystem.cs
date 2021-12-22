using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using System.Collections.Generic;
using Overengineering.Maths;
using Overengineering.UI;

namespace Overengineering
{
    public class KinematicEntitySystem : EntitySystem<KinematicEntity>
    {
        public override void Update(GameTime gameTime)
        {
            //for now n^2 cause too lazy for octree rn
            //just wanna test
            foreach (KinematicEntity e in Entities)
            {
                foreach (KinematicEntity e2 in Entities)
                {
                    if (!e.Equals(e2)) Compare(e, e2);
                }
            }
        }

        public void Compare(KinematicEntity Ke, KinematicEntity Ke2)
        {
            foreach (Polygon p in Ke.Polygons)
            {
                foreach (Polygon p2 in Ke2.Polygons)
                {
                    CollisionInfo c = Collision.SAT(p, p2);
                    if(c.d != Vector3.Zero) Logger.NewText(c.d);
                }
            }
        }
    }
}
