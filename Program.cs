using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Overengineering.Assets;
using Overengineering.Assets.Loaders;
using Overengineering.Graphics;
using Overengineering.Scenes;
using System;

namespace Overengineering
{
	public class Program : Game
	{
		public static Game Instance;

		public static EntityFocalCamera Camera { get; set; }

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
			Instance = this;
			Camera = new EntityFocalCamera(null);
		}

		protected override void Update(GameTime gameTime)
        {
			Time.GameUpdateCount++;
			//Test

			SceneHolder.Update(gameTime);
		}

		protected override void LoadContent()
		{
			AssetServer.Start(Content);
			Renderer.PrepareRenderer();

			RegisterLayers();

			SceneHolder.StartScene(new TestScene());
		}

		public void RegisterLayers()
        {
			LayerHost.RegisterLayer(new Layer(0, null, Camera), "Models");
			LayerHost.RegisterLayer(new Layer(0, null, Camera), "Default");
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.DrawScene(SceneHolder.CurrentScene);
			base.Draw(gameTime);
		}
	}
}
