using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public static void InitializeGraphics(Game game)
		{
            Instance = game;

            GraphicsDeviceManager = new GraphicsDeviceManager(Instance)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };

            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.ApplyChanges();
        }

        public static void PrepareRenderer()
		{
            RenderTarget = new RenderTarget2D(GraphicsDeviceManager.GraphicsDevice, 2560, 1440);
            Spritebatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        public static void Draw()
        {
            Spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Spritebatch.Draw(RenderTarget, default(Rectangle), Color.White);

            Spritebatch.End();
        }
    }
}
