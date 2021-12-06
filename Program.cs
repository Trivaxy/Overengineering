using Microsoft.Xna.Framework;

namespace Overengineering
{
	public class Program : Game
	{
		public static Game Instance;

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
			Renderer.PrepareRenderer();
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.Draw();
			base.Draw(gameTime);
		}
	}
}
