using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.UI;

namespace Overengineering.Scenes
{
	public class TestScene : Scene
	{
        public static ModelComponent Tree { get; set; }
        public static QuadMesh Floor;
        public static UIQuad TestUIQuad;

        private readonly int FloorSize = 10000;

        public override void OnActivate()
        {
            Tree = new ModelComponent(Assets<Model>.Get("Models/Tree"));
            Floor = new QuadMesh(
                new Vector3(-FloorSize, 0, -FloorSize), 
                new Vector3(-FloorSize, 0, FloorSize), 
                new Vector3(FloorSize, 0, FloorSize), 
                new Vector3(FloorSize, 0, -FloorSize), 
                Color.AliceBlue, default, 
                Assets<Texture2D>.Get("Textures/Floor"));
            TestUIQuad = new UIQuad(new Vector3(0, 100, 0), new Vector3(0, 0, 0), new Vector3(100, 0, 0), new Vector3(100, 100, 0), Color.Green);

            SceneHolder.AddEntity(Tree);
            Drawables.Add(Floor);
            Drawables.Add(TestUIQuad);

            Tree.Transform.Scale = new Vector3(1);
            Tree.Transform.Position = new Vector3(0);

            Renderer.DefaultCamera.Transform.Position.Y += 100;
        }
    }
}