using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.Utilities;
using System;

namespace Overengineering.UI
{
	public class UIQuad : UIElement
	{
		private QuadMesh Mesh;

		public UIQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Color color)
		{
			Mesh = new QuadMesh(v1, v2, v3, v4, color, "UI", null);
		}

		public override void OnUpdate(GameTime time)
		{
			Vector2 pv1 = Mesh.TopLeft.Position.Project(Renderer.UICamera);
			Vector2 pv2 = Mesh.TopRight.Position.Project(Renderer.UICamera);
			Vector2 pv3 = Mesh.BottomLeft.Position.Project(Renderer.UICamera);
			Vector2 pv4 = Mesh.BottomRight.Position.Project(Renderer.UICamera);

			Rectangle boundingBox = Utils.BoundingBox(pv1, pv2, pv3, pv4);
			Position = new Vector2(boundingBox.X, boundingBox.Y);
			Size = new Vector2(boundingBox.Width, boundingBox.Height);
			Console.WriteLine(boundingBox);
		}

		public override void Draw(SpriteBatch sb) => Mesh.Draw(sb);
	}
}