using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Overengineering.Graphics
{
    public class ModelHost
    {
        internal static List<ModelComponent> Models = new List<ModelComponent>();

        internal static event Action<SpriteBatch> DrawCalls;

        public static RenderTarget2D ModelTarget { get; set; }

        public void OnLoad()
        {
            ModelTarget = 
                new RenderTarget2D(
                Renderer.GraphicsDeviceManager.GraphicsDevice, 
                Renderer.MaxResolution.X, Renderer.MaxResolution.Y);
        }

        public static void SubscribeCall(Action<SpriteBatch> del) => DrawCalls += del;

        public void DrawTarget(SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            sb.Draw(ModelTarget, Renderer.Destination, Color.White);

            sb.End();
        }
        public void DrawModels(SpriteBatch sb)
        {
            RenderTargetBinding[] oldtargets1 = Renderer.GraphicsDeviceManager.GraphicsDevice.GetRenderTargets();

            GraphicsDevice GD = Renderer.GraphicsDeviceManager.GraphicsDevice;

            GD.SetRenderTarget(ModelTarget);
            GD.Clear(Color.Transparent);

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            DrawCalls?.Invoke(sb);
            DrawCalls = null;

            sb.End();
            GD.SetRenderTargets(oldtargets1);
        }
    }
}