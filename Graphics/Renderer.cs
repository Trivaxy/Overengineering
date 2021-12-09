using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Scenes;

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
            RenderTarget = new RenderTarget2D(GraphicsDeviceManager.GraphicsDevice, MaxResolution.X, MaxResolution.Y, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
            Spritebatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        public static void DrawScene(Scene scene)
        {
            DrawSceneToTarget(scene);

            Spritebatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Spritebatch.Draw(RenderTarget, Destination, Color.White);
            SceneHolder.DrawTransition(Spritebatch); // won't do anything if no transition is active

            Spritebatch.End();
        }

        private static void DrawSceneToTarget(Scene scene)
        {
            GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);

            LayerHost.DrawLayers(scene, Spritebatch);

            GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
