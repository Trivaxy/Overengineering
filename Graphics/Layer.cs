using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overengineering.Resources;
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

        public Layer(float priority = 0, Effect effect = null, CameraTransform camera = default)
        {
            LayerEffect = effect;
            Priority = priority;
            Camera = camera;
        }

        public void AppendCall(Action<SpriteBatch> call) => DrawCalls += call;

        public void AppendPrimitiveCall(Action<SpriteBatch> call) => PrimitiveCalls += call;

        public void Draw(SpriteBatch sb)
        {
            //FNA has shitty overloads smh
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, LayerEffect, Camera.TransformationMatrix);

            LayerEffect?.CurrentTechnique.Passes[0]?.Apply();

            DrawCalls?.Invoke(sb);

            sb.End();

            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            Point viewportSize = Renderer.ViewportSize;
            Vector3 scale = Camera.Transform.Scale;
            Matrix view = Matrix.CreateLookAt(Camera.Transform.Position, Camera.Target, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Renderer.Viewport.AspectRatio, 1f, 4000.11f);
            BasicEffect basicEffect = Assets<Effect>.Get("BasicEffect").GetValue() as BasicEffect;

            basicEffect.View = view;
            basicEffect.Projection = projection;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                pass.Apply();

            PrimitiveCalls?.Invoke(sb);

            sb.End();

            DrawCalls = null;
            PrimitiveCalls = null;
        }
    }
}