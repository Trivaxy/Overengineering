using Microsoft.Xna.Framework;
using System;

namespace Overengineering
{
	public class Program : Game
	{
		public static Renderer Renderer { get; set; }
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
			Renderer = new Renderer(this);
			Window.AllowUserResizing = true;
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			IsFixedTimeStep = true;
		}

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

			Renderer.Draw();
		}
	}
}
