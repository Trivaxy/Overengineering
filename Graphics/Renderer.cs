using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Overengineering
{
    public static class Renderer
    {
        public static Point MaxResolution => new Point(2560, 1440);

        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public static RenderTarget2D RenderTarget { get; private set; }

        public static Game Instance { get; private set; }

        public static SpriteBatch Spritebatch { get; private set; }

        public static Rectangle Destination { get; set; }

        public static void InitializeGraphics(Game game)
		{
            Instance = game;

            GraphicsDeviceManager = new GraphicsDeviceManager(Instance)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };

            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.ApplyChanges();

            Destination = GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds;
        }

        public static void PrepareRenderer()
		{
            RenderTarget = new RenderTarget2D(GraphicsDeviceManager.GraphicsDevice, MaxResolution.X, MaxResolution.Y);
            Spritebatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        public static void Draw()
        {
            DrawToTarget();

            Spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Spritebatch.Draw(RenderTarget, Destination, Color.White);

            Spritebatch.End();
        }

        public static void DrawToTarget()
        {
            GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);

            Spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Program.Tree.Position = new Vector3(500,1000,0);
            Program.Tree.Draw(Spritebatch);

            Spritebatch.End();

            GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
