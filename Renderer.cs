using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Overengineering
{
    public class Renderer
    {
        public virtual Point MaxResolution { get; set; } = new Point(2560, 1440);

        public GraphicsDeviceManager Graphics { get; set; }
        public RenderTarget2D RenderTarget { get; set; }
        public Game Instance { get; set; }
        public SpriteBatch Spritebatch { get; set; }


        public Rectangle RenderDestination;

        public Renderer(Game game)
        {
            Instance = game;

            Graphics = new GraphicsDeviceManager(Instance)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };
            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.ApplyChanges();

            int X = MaxResolution.X;
            int Y = MaxResolution.Y;

            RenderTarget = new RenderTarget2D(Graphics?.GraphicsDevice, X, Y);
            Spritebatch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        public void Draw()
        {
            Spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Spritebatch.Draw(RenderTarget, RenderDestination, Color.White);

            Spritebatch.End();
        }
    }
}
