using Microsoft.Xna.Framework;
using Overengineering.Resources;
using Overengineering.Scenes;

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
			LayerHost.RegisterLayer(new Layer(1, null, null), "UI");
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.DrawScene(SceneHolder.CurrentScene);
			base.Draw(gameTime);
		}
	}
}
