using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Overengineering.Assets;
using Overengineering.Graphics;
using System;
using System.Diagnostics;

namespace Overengineering
{
	public class Program : Game
	{
		public static Game Instance;

		public static World World { get; set; }
		public static CameraTransform Camera { get; set; }

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

			Instance = this;

			World = new World();
			Camera = new CameraTransform();
		}

		float angleX;
		float angleY;

		float PrevX;
		float PrevY;

		protected override void Update(GameTime gameTime)
        {
			//Test
			KeyboardState k = Keyboard.GetState();

			float MoveSpeed = 7;
			float LookAroundSpeed = 0.002f;

			World.Update(gameTime);
			Camera.Update(gameTime);

			float DeltaX = Mouse.GetState().X - PrevX;
			float DeltaY = Mouse.GetState().Y - PrevY;

			if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				angleX -= DeltaX * LookAroundSpeed;
				angleY -= DeltaY * LookAroundSpeed;
			}

			Vector2 directionX = new Vector2((float)Math.Sin(angleX), (float)Math.Cos(angleX));
			Vector2 directionY = new Vector2((float)Math.Sin(angleY), (float)Math.Cos(angleY));

			if (k.IsKeyDown(Keys.W)) Camera.Transform.Position += new Vector3(directionX.X, 0, directionX.Y) * MoveSpeed;
			if (k.IsKeyDown(Keys.S)) Camera.Transform.Position -= new Vector3(directionX.X, 0, directionX.Y) * MoveSpeed;

			if (k.IsKeyDown(Keys.D)) Camera.Transform.Position += new Vector3(-directionX.Y, 0, directionX.X) * MoveSpeed;
			if (k.IsKeyDown(Keys.A)) Camera.Transform.Position -= new Vector3(-directionX.Y, 0, directionX.X) * MoveSpeed;

			Camera.Direction = new Vector3(directionX.X, directionY.X, directionX.Y * directionY.Y);

			PrevX = Mouse.GetState().X;
			PrevY = Mouse.GetState().Y;
		}

		protected override void LoadContent()
		{
			AssetServer.Start(Content);
			Renderer.PrepareRenderer();

			RegisterLayers();

			//Texture = Content.Load<Texture2D>("Textures/LeCat");
			//Model = new ModelComponent(Content.Load<Model>("Models/ExampleModel"));
			Tree = new ModelComponent(Assets<Model>.Get("Models/Tree"));
			World.AddEntity(Tree);

			Tree.Transform.Scale = new Vector3(1);
			Tree.Transform.Position = new Vector3(00);

		}

		public void RegisterLayers()
        {
			LayerHost.RegisterLayer(new Layer(0, null, Camera), "Models");
			LayerHost.RegisterLayer(new Layer(0, null, Camera), "Default");
		}

		protected override void Draw(GameTime gameTime)
        {
			Renderer.Draw(World);
			base.Draw(gameTime);
		}
	}
}
