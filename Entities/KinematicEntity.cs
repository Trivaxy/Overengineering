using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using Overengineering.Maths;
using System.Collections.Generic;
using Overengineering.Graphics.Meshes;
using System.Linq;
using Overengineering.UI;

namespace Overengineering
{
	public class KinematicEntity : RigidBody
	{
		//public bool isStatic...
		//public bool other things I dont know for know
	}

	public class KinematicQuad : KinematicEntity, IDrawable, ITickable
    {
		IList<QuadMesh> Meshes = new List<QuadMesh>();
		public string Layer { get; }
		public KinematicQuad(PhysicsPolygon[] polygons)
        {
			Polygons = polygons.ToList();

			foreach(Polygon poly in polygons)
            {
				Meshes.Add(new QuadMesh(poly.points[0], poly.points[1], poly.points[2], poly.points[3], Color.White));
            }
        }

		public void Update(GameTime gameTime)
        {
			for(int i = 0; i < Polygons.Count; i++)
            {
				Meshes[i].Construct(
					Polygons[i].points[0].point, 
					Polygons[i].points[1].point, 
					Polygons[i].points[2].point,
					Polygons[i].points[3].point, 
					Color.Purple);
			}
		}

		public void Draw(SpriteBatch sb)
        {
			foreach(QuadMesh mesh in Meshes) 
				mesh.Draw(sb);			
        }
    }
}
