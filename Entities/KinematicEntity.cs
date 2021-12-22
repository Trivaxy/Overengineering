using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using Overengineering.Maths;
using System.Collections.Generic;
using Overengineering.Graphics.Meshes;
using System.Linq;

namespace Overengineering
{
	public class KinematicEntity : Entity
	{
		public IEnumerable<Polygon> Polygons = new List<Polygon>();
	}

	public class KinematicQuad : KinematicEntity, IDrawable
    {
		IList<QuadMesh> Meshes = new List<QuadMesh>();
		public string Layer { get; }
		public KinematicQuad(Polygon[] polygons)
        {
			Polygons = polygons.ToList();

			foreach(Polygon poly in polygons)
            {
				Meshes.Add(new QuadMesh(poly.points[0], poly.points[1], poly.points[2], poly.points[3], Color.White));
            }
        }
		public void Draw(SpriteBatch sb)
        {
			foreach(QuadMesh mesh in Meshes) 
				mesh.Draw(sb);			
        }
    }
}
