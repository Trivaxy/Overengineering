using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Assets;
using Overengineering.Graphics;

namespace Overengineering
{
	public class Program : Game
	{
		public static Game Instance;
		public static ModelComponent Model { get; set; }
		public static ModelComponent Tree { get; set; }
		public static Texture2D Texture { get; set; }

		static void Main(string[] args)
		{
			using (Program program = new Program())
			{
				program.Run();
			}
		}

		public Program()
		{
			Renderer.InitializeGraphics(this);
			Window.AllowUserResizing = true;
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			IsFixedTimeStep = true;
		}

		protected override void LoadContent()
		{
			AssetServer.Start(Content);
			Renderer.PrepareRenderer();
			//Texture = Content.Load<Texture2D>("Textures/LeCat");
			//Model = new ModelComponent(Content.Load<Model>("Models/ExampleModel"));
			Tree = new ModelComponent(Assets<Model>.Get("Models/Tree"));
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.Draw();
			base.Draw(gameTime);
		}
	}
}
