using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Overengineering
{
    public class Layer
    {
        private event Action<SpriteBatch> DrawCalls;
        private event Action<SpriteBatch> PrimitiveCalls;

        public Effect LayerEffect { get; init; }

        public CameraTransform Camera { get; init; }

        public float Priority { get; init; }

        public Layer(float priority, CameraTransform camera, Effect effect = null)
        {
            LayerEffect = effect;
            Priority = priority;
            Camera = camera;
        }

        public void AppendCall(Action<SpriteBatch> call) => DrawCalls += call;

        public void AppendPrimitiveCall(Action<SpriteBatch> call) => PrimitiveCalls += call;

        public void Draw(SpriteBatch sb)
        {
            Camera.Update(Time.Current);

            //FNA has shitty overloads smh
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
    }
}