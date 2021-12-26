using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
using Overengineering.Scenes;
using Overengineering.UI;
using System;

namespace Overengineering
{
	public class Program : Game
	{
		[STAThread]
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

		protected override void Update(GameTime gameTime)
        {
			Time.GameUpdateCount++;
			Time.Current = gameTime;

			SceneHolder.Update(gameTime);
		}

		protected override void LoadContent()
		{
			AssetServer.Start(Content);
			Renderer.PrepareRenderer();
			SceneHolder.StartScene(new MenuScene());
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.DrawScene(SceneHolder.CurrentScene);
			base.Draw(gameTime);
		}
	}
}
