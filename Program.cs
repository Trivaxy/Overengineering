using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics;
using Overengineering.Loaders;

namespace Overengineering
{
	public class Program : Game
	{
		public static Game Instance;
		public static ModelComponent Model { get; set; }
		public static ModelComponent Tree { get; set; }
		public static Texture2D Texture { get; set; }

		public AssetLoader<Texture2D> TextureLoader { get; set; }
		public AssetLoader<Model> ModelLoader { get; set; }

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

			TextureLoader = new AssetLoader<Texture2D>("Textures", new string[] { ".xnb" });
			ModelLoader = new AssetLoader<Model>("Models", new string[] { ".xnb" });
		}

		protected override void LoadContent()
		{
			Renderer.PrepareRenderer();
			Texture = Content.Load<Texture2D>("Textures/LeCat");
			Model = new ModelComponent(Content.Load<Model>("Models/ExampleModel"));
			Tree = new ModelComponent(Content.Load<Model>("Models/Tree"));
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.Draw();
			base.Draw(gameTime);
		}
	}
}
