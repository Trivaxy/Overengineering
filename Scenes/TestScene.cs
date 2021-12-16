using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.UI;
using FontStashSharp;

namespace Overengineering.Scenes
{
	public class TestScene : Scene
	{
        public static ModelComponent Tree { get; set; }
        public static QuadMesh Floor;
        public static UIQuad TestUIQuad;
        public static OnScreenLogger ScreenLogger;
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

            TestUIQuad = new UIQuad(new Vector3(300, 400, 0), new Vector3(300, 300, 0), new Vector3(400, 300, 0), new Vector3(400, 400, 0), Color.Green);
            ScreenLogger = new OnScreenLogger();

            SceneHolder.AddEntity(Tree);

            Drawables.Add(Floor);
            Drawables.Add(TestUIQuad);
            Tickables.Add(TestUIQuad);
            Drawables.Add(ScreenLogger);
            Tickables.Add(ScreenLogger);

            Tree.Transform.Scale = new Vector3(1);
            Tree.Transform.Position = new Vector3(0);

            Renderer.DefaultCamera.Transform.Position.Y += 100;
        }
    }

    public class TestText : IDrawable
	{
        public FontSystem Font;
        public string Text;
        public int FontSize;
        public Vector2 Position;
        public Color Color;

        public TestText(FontSystem font, string text, int fontSize, Vector2 position, Color color)
		{
            Font = font;
            Text = text;
            FontSize = fontSize;
            Position = position;
            Color = color;
		}

        public string Layer => "UI";

		public void Draw(SpriteBatch sb)
		{
            Font.GetFont(FontSize).DrawText(sb, Text, Position, Color);
		}
	}
}