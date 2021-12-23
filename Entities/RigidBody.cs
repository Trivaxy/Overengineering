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
	public class RigidBody : Entity
	{
		public IList<PhysicsPolygon> Polygons = new List<PhysicsPolygon>();

		public float mass = 0.1f;
		public float resistution = 1f;
		public float airResistance = 1f;
	}
}
