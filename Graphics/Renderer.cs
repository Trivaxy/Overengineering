using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics.Meshes;
using Overengineering.Resources;
using Overengineering.Scenes;

namespace Overengineering
{
    public static class Renderer
    {
        public static Point MaxResolution => new Point(2560, 1440);

        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public static GraphicsDevice Device => GraphicsDeviceManager.GraphicsDevice;

        public static Viewport Viewport => Device.Viewport;

        public static Point ViewportSize => new Point(Viewport.Width, Viewport.Height);

        public static RenderTarget2D RenderTarget { get; private set; }

        public static Game Instance { get; private set; }

        public static SpriteBatch Spritebatch { get; private set; }

        public static Rectangle Destination { get; set; }

        public static EntityFocalCamera DefaultCamera { get; set; }

        public static CameraTransform UICamera { get; set; }

        public static void InitializeGraphics(Game game)
		{
            Instance = game;

            GraphicsDeviceManager = new GraphicsDeviceManager(Instance)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };

            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.ApplyChanges();

            Destination = Device.Viewport.Bounds;
        }

        public static void PrepareRenderer()
		{
            InitializeCameras();
            RegisterLayers();

            Assets<Effect>.Register("BasicEffect", new BasicEffect(Device));

            RenderTarget = new RenderTarget2D(Device, MaxResolution.X, MaxResolution.Y, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
            Spritebatch = new SpriteBatch(Device);
        }

        public static void InitializeCameras()
		{
            DefaultCamera = new EntityFocalCamera(null, Vector3.Zero);
            UICamera = new CameraTransform(Vector3.UnitZ);
        }

        public static void RegisterLayers()
        {
            LayerHost.RegisterLayer(new Layer(0, DefaultCamera), "Models");
            LayerHost.RegisterLayer(new Layer(0, DefaultCamera), "Default");
            LayerHost.RegisterLayer(new Layer(1, UICamera), "UI");
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
            Device.SetRenderTarget(RenderTarget);
            Device.Clear(Color.Transparent);

            LayerHost.DrawLayers(scene, Spritebatch);

            Device.SetRenderTarget(null);
        }
    }
}
