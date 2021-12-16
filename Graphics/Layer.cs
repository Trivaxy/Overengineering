using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Overengineering
{
    public class Layer
    {
        private event Action<SpriteBatch> DrawCalls;
        private event Action<SpriteBatch> PrimitiveCalls;

        private RenderTarget2D Target { get; set; }

        public Effect LayerEffect { get; init; }

        public CameraTransform Camera { get; init; }

        public float Priority { get; init; }

        public Rectangle ScissorSource { get; set; }

        public Rectangle Destination { get; set; }

        public Layer(float priority, CameraTransform camera, Effect effect = null, Rectangle scissor = default, Rectangle destination = default)
        {
            LayerEffect = effect;
            Priority = priority;
            Camera = camera;

            ScissorSource = scissor == default ? Renderer.MaxResolutionBounds : scissor;
            Destination = destination == default ? Renderer.MaxResolutionBounds : destination;

            Target = new RenderTarget2D(Renderer.Device, Renderer.MaxResolution.X, Renderer.MaxResolution.Y, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8);
        }

        public virtual void OnDraw() { }

        public void AppendCall(Action<SpriteBatch> call) => DrawCalls += call;

        public void AppendPrimitiveCall(Action<SpriteBatch> call) => PrimitiveCalls += call;

        public void DrawToTarget(SpriteBatch sb)
        {
            Camera.Update(Time.Current);

            Renderer.Device.SetRenderTarget(Target);
            Renderer.Device.Clear(Color.Transparent);

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, LayerEffect, Camera.TransformationMatrix);

            LayerEffect?.CurrentTechnique.Passes[0]?.Apply();
            DrawCalls?.Invoke(sb);

            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            PrimitiveCalls?.Invoke(sb);
            sb.End();

            DrawCalls = null;
            PrimitiveCalls = null;
        }

        public void Draw(SpriteBatch sb)
        {
            OnDraw();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            sb.Draw(Target, Destination, ScissorSource, Color.White);

            sb.End();
        }
    }
}