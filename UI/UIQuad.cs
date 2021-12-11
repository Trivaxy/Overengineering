using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.Utilities;

namespace Overengineering.UI
{
	public class UIQuad : UIElement
	{
		private QuadMesh Mesh;

		public UIQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
		{
			Vector2 pv1 = v1.Project(Renderer.UICamera);
			Vector2 pv2 = v2.Project(Renderer.UICamera);
			Vector2 pv3 = v3.Project(Renderer.UICamera);
			Vector2 pv4 = v4.Project(Renderer.UICamera);

			Rectangle boundingBox = Utils.BoundingBox(pv1, pv2, pv3, pv4);
			Position = new Vector2(boundingBox.X, boundingBox.Y);
			Size = new Vector2(boundingBox.Width, boundingBox.Height);
			Mesh = new QuadMesh(v1, v2, v3, v4, color, "UI", null);
		}

		public override void Draw(SpriteBatch sb) => Mesh.Draw(sb);
	}
}